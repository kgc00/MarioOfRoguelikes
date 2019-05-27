using UnityEngine;

public class Tile {
    public GameObject Holder { get; protected set; }
    public Vector2Int Position { get; protected set; }
    public TileType Type { get; protected set; }

    public Tile (Vector2Int pos, TileType type, GameObject holder) {
        Position = pos;
        Type = type;
        Holder = holder;
    }
}