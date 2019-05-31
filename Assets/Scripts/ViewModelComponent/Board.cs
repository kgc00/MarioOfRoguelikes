using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public BoardBehaviour Container { get; set; }

    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    private Dictionary<Vector2Int, Unit> units = new Dictionary<Vector2Int, Unit>();
    private List<Unit> deletedUnits = new List<Unit>();
    struct LateTriggerData
    {
        public Action<Unit, Tile> trigger;
        public Unit unit;
        public Tile tile;

        public LateTriggerData(Action<Unit, Tile> trigger, Unit unit, Tile tile)
        {
            this.trigger = trigger;
            this.unit = unit;
            this.tile = tile;
        }

    }
    List<LateTriggerData> lateTriggers = new List<LateTriggerData>();

    public Tile TileAt(Vector2Int pos)
    {
        Tile tile = null;
        tiles.TryGetValue(pos, out tile);
        return tile;
    }

    public Unit UnitAt(Vector2Int pos)
    {
        Unit unit = null;
        units.TryGetValue(pos, out unit);
        return unit;
    }

    public void Move(Unit unit, Vector2Int pos)
    {
        Tile tileWeAreLeaving = TileAt(unit.Position);
        if (tileWeAreLeaving.Trigger != null)
        {
            lateTriggers.Add(new LateTriggerData(tileWeAreLeaving.Trigger.OnLeave, unit, tileWeAreLeaving));
        }

        units.Remove(unit.Position);
        unit.Position = pos;
        units.Add(unit.Position, unit);
        Tile tileWeSteppedOn = TileAt(pos);

        if (tileWeSteppedOn.Trigger != null)
        {
            lateTriggers.Add(new LateTriggerData(tileWeSteppedOn.Trigger.OnEnter, unit, tileWeSteppedOn));
        }

    }

    public void DeleteUnitAt(Vector2Int pos)
    {
        deletedUnits.Add(units[pos]);
        BoardHelper.DeleteUnitAt(pos, ref units);
    }

    public void Load()
    {
        if (Container.levelData == null)
            return;

        foreach (TileSpawnData data in Container.levelData.tiles)
        {
            Tile tile = BoardHelper.CreateTile(
                Container.transform, data.tileType.Prefab,
                data.location, data.tileType);

            tiles.Add(tile.Position, tile);
        }

        foreach (UnitSpawnData data in Container.levelData.units)
        {
            Unit unit = BoardHelper.CreateUnit(
                Container.transform, this,
                data.location, data.unitType);

            units.Add(unit.Position, unit);
        }
    }

    public void Tick()
    {
        // TODO: probably exit here for PAUSING

        // make all the units to their things
        List<Unit> unitsCopy = new List<Unit>(units.Values);
        foreach (Unit unit in unitsCopy)
        {
            if (!deletedUnits.Contains(unit))
            {
                unit.Tick();
            }
        }

        foreach (LateTriggerData call in lateTriggers)
        {
            if (!deletedUnits.Contains(call.unit))
            {
                call.trigger(call.unit, call.tile);
            }

        }

        if (lateTriggers.Count > 0)
        {
            lateTriggers.Clear();
        }

        if (deletedUnits.Count > 0)
        {
            deletedUnits.Clear();
        }

    }
}