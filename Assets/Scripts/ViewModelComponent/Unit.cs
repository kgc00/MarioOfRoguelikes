using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit {
    public UnitBehaviour Behaviour { get; protected set; }
    public UnitType Type { get; protected set; }
    public BaseAI AI { get; protected set; }
    public Board Board { get; protected set; }
    public Vector2Int Position { get; set; }
    public Energy energy;
    private bool isDying;
    Coroutine routine;

    public Unit (Board board, UnitBehaviour unitBehaviour, UnitType type, BaseAI ai, Vector2Int position) {
        Board = board;
        Behaviour = unitBehaviour;
        Type = type;
        Position = position;
        AI = ai;
        isDying = false;

        // TODO: refactor into Stats system
        energy = type.energyInitializer != null ?
            new Energy (type.energyInitializer) :
            new Energy ();
    }

    // ~Unit()
    // {
    //     if (routine != null)
    //     {
    //         CoroutineHelper.Instance.Stop(routine);
    //     }
    // }

    public void ClearStuff () {
        if (routine != null) {
            CoroutineHelper.Instance.Stop (routine);
        }
    }

    public void Tick () {

        // TODO: Refactor into stat system
        energy.Tick ();

        TakeAction ();

        NotificationCenter.FireEvent<StatChange> (new StatChange (this, typeof (Energy), energy.Bars));
        Render ();
        CheckHeroOutOfEnergy ();
    }

    private void CheckHeroOutOfEnergy () {
        if (AI.IsHero () && isDying && energy.Current > 0) {
            // stop death
            CoroutineHelper.Instance.Stop (routine);
            isDying = false;
        }

        if (AI.IsHero () && energy.Current == 0 && !isDying) {
            // QueueDeath
            routine = CoroutineHelper.Instance.Countdown (.5f, .1f, () => OnDeath ());
            isDying = true;
        }
    }

    private void TakeAction () {
        if (AI == null) {
            return;
        }

        Action action = AI.TakeTurn ();

        if (action != null && action.Cost <= energy.Bars) {
            ActionResult result;
            do {
                result = action.Perform (this);
                if (result.Type == ActionResultType.ALTERNATE) {
                    action = result.AlternateAction;
                }
            } while (result.Type == ActionResultType.ALTERNATE);

            if (result.Type == ActionResultType.SUCCESS) {

                energy.Spend (action.Cost);
            }
        }
    }

    private void Render () {
        Behaviour.UpdatePosition (Position);
        Behaviour.UpdateSprite (Type.Image);
        if (this.AI is HeroAI) {
            Behaviour.MatchSizeToEnergy (UnityEngine.Mathf.Lerp (0.5f, 1, (energy.Current / energy.Max)));
        }
    }

    protected virtual Action TakeTurn () { return null; }

    public virtual void OnDeath () {
        if (AI.IsHero ()) {
            Tile tile = Board.TileAt (Position);
            if (tile.Trigger != null) {
                tile.Trigger.OnLeave (this, tile);
            }
            GameManager.Instance.Reload ();
        }
        Object.Destroy (this.Behaviour.gameObject);
    }
}