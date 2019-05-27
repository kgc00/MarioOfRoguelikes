using UnityEngine;

public class Tile
{
    public TileBehaviour Behaviour { get; protected set; }
    public Vector2Int Position { get; protected set; }
    public TileType Type { get; protected set; }

    public Tile(TileBehaviour behaviour, Vector2Int pos, TileType type)
    {
        Behaviour = behaviour;
        Position = pos;
        Type = type;
    }
}