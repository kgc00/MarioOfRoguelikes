using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapTrigger : BaseTrigger
{
    float rate;
    Vector2Int direction;
    Coroutine routine;
    Unit arrow;
    // reference to the type of unit to spawn

    public ArrowTrapTrigger(ArrowTrapTriggerData data)
    {
        this.rate = data.Rate;
        this.direction = data.Direction;
        this.arrow = data.Arrow;
        this.TriggerTrap();
    }

    void TriggerTrap()
    {
        // spawn a unit
        BoardHelper.Instance.CreateUnit(
            Transform: this, // need a real value
            Board: Arrow.Board,
            Vector2Int: (direction + this.pos),
            UnitType: arrow.Type);

        routine = CoroutineHelper.Instance
            .Countdown(Rate, .1f, () => TriggerTrap());
    }

    public void OnEnter(Unit unit, Tile tile)
    {
    }

    public void OnLeave(Unit unit, Tile tile)
    {
    }
}