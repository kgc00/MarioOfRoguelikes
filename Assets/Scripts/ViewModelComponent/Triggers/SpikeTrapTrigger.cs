using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can refactor to have traps trigger after
// the timer ends, even when a unit leaves
public class SpikeTrapTrigger : BaseTrigger
{
    float triggerTimer;

    Coroutine routine;

    TileType spikeTile;
    TileType groundTile;

    public SpikeTrapTrigger(SpikeTrapTriggerData data)
    {
        this.triggerTimer = data.TriggerTimer;
        this.spikeTile = data.SpikeTile;
        this.groundTile = data.GroundTile;
    }

    void TriggerTrap(Unit unit, Tile tile)
    {
        tile.Board.PlaceTile(tile.Position, spikeTile);
        if (unit.Position == tile.Position)
        {
            unit.Board.DeleteUnitAt(unit.Position);
        }

    }

    public void OnEnter(Unit unit, Tile tile)
    {
        routine = CoroutineHelper.Instance
            .Countdown(triggerTimer, .1f, () => TriggerTrap(unit, tile));
    }

    public void OnLeave(Unit unit, Tile tile)
    {
        tile.Board.PlaceTile(tile.Position, groundTile);
        CoroutineHelper.Instance.Stop(routine);
        routine = null;
    }
    public void StartTimer(Board board, Tile tile) { }
}