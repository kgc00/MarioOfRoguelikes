using UnityEngine;

public class Tile
{
    public TileBehaviour Behaviour { get; protected set; }
    public Vector2Int Position { get; protected set; }
    public TileType Type { get; protected set; }
    public Board Board { get; protected set; }

    public BaseTrigger Trigger { get; protected set; }

    public Tile(Board board, TileBehaviour behaviour, Vector2Int pos, TileType type, BaseTrigger trigger)
    {
        Behaviour = behaviour;
        Position = pos;
        Type = type;
        Trigger = trigger;
        Board = board;
    }
}