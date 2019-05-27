[System.Serializable]
public struct Vector2Int : System.IEquatable<Vector2Int>
{
    public int x;
    public int y;

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }
    public static Vector2Int operator -(Vector2Int p1, Vector2Int p2)
    {
        return new Vector2Int(p1.x - p2.x, p1.y - p2.y);
    }
    public static bool operator ==(Vector2Int a, Vector2Int b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Vector2Int a, Vector2Int b)
    {
        return !(a == b);
    }
    public override bool Equals(object obj)
    {
        if (obj is Vector2Int)
        {
            Vector2Int p = (Vector2Int)obj;
            return x == p.x && y == p.y;
        }
        return false;
    }
    public bool Equals(Vector2Int p)
    {
        return x == p.x && y == p.y;
    }
    public override int GetHashCode()
    {
        return x ^ y;
    }
    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }
}