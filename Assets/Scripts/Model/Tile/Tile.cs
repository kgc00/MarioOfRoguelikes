public class Tile
{
    public Vector2Int Position { get; protected set; }
    public TileType Type { get; protected set; }

    public Tile(Vector2Int pos, TileType type)
    {
        Position = pos;
        Type = type;
    }
}