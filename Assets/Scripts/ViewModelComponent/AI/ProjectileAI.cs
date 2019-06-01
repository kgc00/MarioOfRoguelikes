
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectileAI : PatternAI, BaseAI
{
    Queue<Vector2Int> pattern = new Queue<Vector2Int>();

    public ProjectileAI(ProjectileAIData data)
    {
        for (int i = 0; i < data.Pattern.pattern.Length; i++)
        {
            pattern.Enqueue(data.Pattern.pattern[i]);
        }
        NotificationCenter.AddListener<ActionResultNotification>(OnActionResultNotification);
    }

    public override Action TakeTurn()
    {
        base.TakeTurn();
    }

    private void OnActionResultNotification(ActionResultNotification notification)
    {
        if (notification.AI == this)
        {
            BaseOnActionResultNotification(notification);
            if (notification.Result.Type == ActionResultType.SUCCESS)
            {
                pattern.Enqueue(pattern.Dequeue());
            }
            else
            {
                // BoardHelper.Instance.DeleteUnitAt()
            }
        }
    }

    ~PatternAI()
    {
        NotificationCenter.RemoveListener<ActionResultNotification>(OnActionResultNotification);
    }
}