using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {
    public BoardBehaviour Container { get; set; }

    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile> ();
    private Dictionary<Vector2Int, Unit> units = new Dictionary<Vector2Int, Unit> ();

    public Tile TileAt (Vector2Int pos) {
        Tile tile = null;
        tiles.TryGetValue (pos, out tile);
        return tile;
    }

    public Unit UnitAt (Vector2Int pos) {
        Unit unit = null;
        units.TryGetValue (pos, out unit);
        return unit;
    }

    public void Move (Unit unit, Vector2Int pos) {
        units.Remove (unit.Position);
        unit.Position = pos;
        units.Add (unit.Position, unit);
    }

    public void DeleteUnitAt (Vector2Int pos) {
        BoardHelper.DeleteUnitAt (pos, ref units);
    }

    public void Load () {
        if (Container.levelData == null)
            return;

        foreach (TileSpawnData data in Container.levelData.tiles) {
            Tile tile = BoardHelper.CreateTile (
                Container.transform, Container.TilePrefab,
                data.location, data.tileType);

            tiles.Add (tile.Position, tile);
        }

        foreach (UnitSpawnData data in Container.levelData.units) {
            Unit unit = BoardHelper.CreateUnit (
                Container.transform, Container.UnitPrefab, this,
                data.location, data.unitType);

            units.Add (unit.Position, unit);
        }
    }
}