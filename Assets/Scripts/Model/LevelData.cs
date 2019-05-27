using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData : ScriptableObject {

    public List<TileSpawnData> tiles;

    public List<UnitSpawnData> units;
}

[Serializable]
public struct TileSpawnData {
    public Vector2Int location;
    public TileType tileType;

    public TileSpawnData (Vector2Int _location, TileType _tileType) {
        this.location = _location;
        this.tileType = _tileType;
    }
}

[Serializable]
public struct UnitSpawnData {
    public Vector2Int location;
    public UnitType unitType;

    public UnitSpawnData (Vector2Int _location, UnitType _unitType) {
        this.location = _location;
        this.unitType = _unitType;
    }
}