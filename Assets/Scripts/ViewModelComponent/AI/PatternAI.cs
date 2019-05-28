using System.Collections.Generic;
using UnityEngine;
public class PatternAI : BaseAI
{

    Queue<Vector2Int> pattern = new Queue<Vector2Int>();
    private float moveDelay = .25f;
    private bool mustWait = false;
    private Coroutine routine;

    public PatternAI(PatternAIData data)
    {
        for (int i = 0; i < data.Pattern.pattern.Length; i++)
        {
            pattern.Enqueue(data.Pattern.pattern[i]);
        }
        NotificationCenter.AddListener<ActionResultNotification>(OnActionResultNotification);
    }

    public bool IsHero()
    {
        return false;
    }

    public Action TakeTurn()
    {
        if (mustWait)
        {
            return null;
        }

        return new MoveAction(pattern.Peek(), this);
    }


    void OnCooldownDone()
    {
        mustWait = false;
    }


    private void OnActionResultNotification(ActionResultNotification notification)
    {
        if (notification.AI == this)
        {
            if (notification.Result.Type == ActionResultType.SUCCESS)
            {
                mustWait = true;
                routine = CoroutineHelper.Instance.Countdown(moveDelay, .05f, OnCooldownDone);
            }


            if (notification.Result.Type == ActionResultType.SUCCESS ||
                notification.Result.Type == ActionResultType.FAILURE)
            {
                pattern.Enqueue(pattern.Dequeue());
            }
        }
    }

    ~PatternAI()
    {
        if (routine != null)
        {
            CoroutineHelper.Instance.Stop(routine);
        }
        NotificationCenter.RemoveListener<ActionResultNotification>(OnActionResultNotification);
    }

}