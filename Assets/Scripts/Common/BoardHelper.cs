using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class BoardHelper {
    static public Unit CreateUnit (
        Transform container, UnitBehaviour prefab,
        Board board, Vector2Int pos, UnitType type) {
        UnitBehaviour unitBehaviour =
            Object.Instantiate<UnitBehaviour> (
                prefab, container
            );
        // Unit unit = new Unit (
        //     board, pos, type, unitBehaviour.gameObject
        // );

        Unit unit = null;
        switch (type.Breed) {
            case UnitType.Breeds.MONSTER:
                unit = new Monster (
                    board, pos, type, unitBehaviour.gameObject
                );
                break;
            case UnitType.Breeds.HERO:
                unit = new Hero (
                    board, pos, type, unitBehaviour.gameObject
                );
                break;
        }
        unitBehaviour.SetUnit (unit);
        return unit;
    }

    static public Tile CreateTile (
        Transform container, TileBehaviour prefab,
        Vector2Int pos, TileType type) {
        // Make the two pieces
        TileBehaviour tileBehaviour =
            Object.Instantiate<TileBehaviour> (
                prefab, container
            );
        Tile tile = new Tile (
            pos, type, tileBehaviour.gameObject
        );

        // Connect monobehaviour with tile data
        tileBehaviour.SetTile (tile);
        return tile;
    }

    static public void DeleteUnitAt (
        Vector2Int p, ref Dictionary<Vector2Int, Unit> units) {
        if (units.ContainsKey (p)) {
            Unit unit = units[p];
            Object.Destroy (unit.Holder);
            units.Remove (p);
        }
    }
}