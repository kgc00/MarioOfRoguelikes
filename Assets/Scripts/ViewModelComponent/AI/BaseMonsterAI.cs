using UnityEngine;
public abstract class BaseMonsterAI {
    // TODO: make moveDelay configurable
    protected bool MustWait {
        get {
            return mustWait;
        }
    }
    private float moveDelay = .1f;
    private bool mustWait = false;
    private Coroutine routine;

    public bool IsHero () {
        return false;
    }

    private void OnCooldownDone () {
        mustWait = false;
    }

    protected void BaseOnActionResultNotification (ActionResultNotification notification) {
        if (notification.Result.Type == ActionResultType.SUCCESS) {
            mustWait = true;
            routine = CoroutineHelper.Instance.Countdown (moveDelay, .05f, OnCooldownDone);
        }
    }

    // ~BaseMonsterAI()
    // {
    //     if (routine != null)
    //     {
    //         CoroutineHelper.Instance.Stop(routine);
    //     }
    // }
}