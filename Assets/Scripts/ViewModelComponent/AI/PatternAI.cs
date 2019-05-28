using System.Collections.Generic;
using UnityEngine;
public class PatternAI : BaseAI {

    Queue<Vector2Int> pattern = new Queue<Vector2Int> ();

    public PatternAI (PatternAIData data) {
        for (int i = 0; i < data.Pattern.pattern.Length; i++) {
            pattern.Enqueue (data.Pattern.pattern[i]);
        }
        NotificationCenter.AddListener<ActionResultNotification> (OnActionResultNotification);
    }

    public bool IsHero () {
        return false;
    }

    public Action TakeTurn () {
        return new MoveAction (pattern.Peek (), this);
    }

    private void OnActionResultNotification (ActionResultNotification notification) {
        if (notification.AI == this) {
            if (notification.Result.Type == ActionResultType.SUCCESS ||
                notification.Result.Type == ActionResultType.FAILURE) {
                pattern.Enqueue (pattern.Dequeue ());
            }
        }
    }

    ~PatternAI () {
        NotificationCenter.RemoveListener<ActionResultNotification> (OnActionResultNotification);
    }

}