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

    public Unit (Board board, UnitBehaviour unitBehaviour, UnitType type, BaseAI ai, Vector2Int position) {
        Board = board;
        Behaviour = unitBehaviour;
        Type = type;
        Position = position;
        AI = ai;

        // TODO: refactor into Stats system
        energy = type.energyInitializer != null   
            ? new Energy (type.energyInitializer) 
            : new Energy();
    }

    public void Tick () {

        // TODO: Refactor into stat system
        energy.Tick ();

        TakeAction ();

        NotificationCenter.FireEvent<StatChange> (new StatChange (this, typeof (Energy), energy.Bars));
        Render ();
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
    }

    protected virtual Action TakeTurn () { return null; }

    public virtual void OnDeath () {
        if (AI.IsHero ())
            GameManager.Instance.Reload ();

        Object.Destroy(this.Behaviour.gameObject);
    }
}