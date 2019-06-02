using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapTrigger : BaseTrigger
{
    float rate;
    Vector2Int direction;
    Coroutine routine;
    UnitType arrow;
    // reference to the type of unit to spawn

    public ArrowTrapTrigger(ArrowTrapTriggerData data)
    {
        this.rate = data.Rate;
        this.direction = data.Direction;
        this.arrow = data.Arrow;
    }

    public void StartTimer(Board board, Tile tile)
    {
        routine = CoroutineHelper.Instance
            .Countdown(rate, .1f, () => onTimerComplete(board, tile));
    }

    private void onTimerComplete(Board board, Tile tile)
    {
        if (board.Container != null)
        {
            board.PlaceUnit(tile.Position, arrow);
            Unit unit = board.UnitAt(tile.Position);
            if (unit != null && (unit.AI is ProjectileAI))
            {
                (unit.AI as ProjectileAI).ChangeDirection(direction);
            }
            else
            {
                Debug.Log("ArrowTrap did not spawn a unit that has ProjectileAI.");
            }
            StartTimer(board, tile);
        }
    }

    public void OnEnter(Unit unit, Tile tile)
    {
    }

    public void OnLeave(Unit unit, Tile tile)
    {
    }

    ~ArrowTrapTrigger()
    {
        if (routine != null)
        {
            CoroutineHelper.Instance.Stop(routine);
        }
    }
}