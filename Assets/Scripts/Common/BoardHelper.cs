using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class BoardHelper
{
    static public Unit CreateUnit(
        Transform container,
        Board board,
        Vector2Int pos,
        UnitType type)
    {
        UnitBehaviour unitBehaviour = Object.Instantiate<UnitBehaviour>(type.Prefab, container);

        BaseAI ai = null;
        if (type.AI == UnitType.AIs.HERO)
        {
            ai = new HeroAI();
        }
        else if (type.AI == UnitType.AIs.PATTERN)
        {
            ai = new PatternAI(type.PatternAIData);
        }

        Unit unit = new Unit(board, unitBehaviour, type, ai, pos);

        unitBehaviour.UpdateSprite(type.Image);
        unitBehaviour.UpdatePosition(pos);

        return unit;
    }

    static public Tile CreateTile(
        Transform container, Board board, TileBehaviour prefab,
        Vector2Int pos, TileType type)
    {
        // Make the two pieces
        TileBehaviour tileBehaviour = Object.Instantiate<TileBehaviour>(type.Prefab, container);

        BaseTrigger trigger = null;
        if (type.TriggerType == TileType.TriggerTypes.ENERGY_REFILL)
        {
            trigger = new EnergyFillTrigger(type.energyRefillTriggerData);
        }
        else if (type.TriggerType == TileType.TriggerTypes.GOAL)
        {
            trigger = new GoalTrigger();
        }
        else if (type.TriggerType == TileType.TriggerTypes.SPIKE_TRAP)
        {
            trigger = new SpikeTrapTrigger(type.spikeTrapTriggerData);
        }
        else if (type.TriggerType == TileType.TriggerTypes.SWITCH)
        {
            trigger = new SwitchTrigger(type.switchTriggerData);
        }

        Tile tile = new Tile(board, tileBehaviour, pos, type, trigger);

        tileBehaviour.UpdateSprite(type.Image);
        tileBehaviour.UpdatePosition(pos);

        return tile;
    }

    static public void DeleteUnitAt(
        Vector2Int p, ref Dictionary<Vector2Int, Unit> units)
    {
        if (units.ContainsKey(p))
        {
            units[p].OnDeath();
            units.Remove(p);
        }
    }

    static public void DeleteTileAt(
        Vector2Int p, ref Dictionary<Vector2Int, Tile> tiles)
    {
        if (tiles.ContainsKey(p))
        {
            Tile tile = tiles[p];
            tiles.Remove(p);
            Object.Destroy(tile.Behaviour.gameObject);
        }

    }
}