
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChaseAI : BaseMonsterAI, BaseAI
{
    Board board;
    Unit unit;

    public ChaseAI(Board board)
    {
        this.board = board;
        NotificationCenter.AddListener<ActionResultNotification>(OnActionResultNotification);
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Action TakeTurn()
    {
        if (MustWait)
        {
            return null;
        }

        Vision(unit.Position, 4);

        return null;

    }


    private Dictionary<Vector2Int, Tile> GetSquareTiles(Vector2Int center, int range)
    {
        Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
        Vector2Int min = new Vector2Int(center.x - range, center.y - range);
        Vector2Int max = new Vector2Int(center.x + range, center.y + range);
        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                tiles.Add(pos, board.TileAt(pos));
            }
        }
        return tiles;
    }

    private void Vision(Vector2Int center, int range)
    {

        Dictionary<Vector2Int, Tile> tiles = GetSquareTiles(center, range);
        Dictionary<Vector2Int, bool> visible = new Dictionary<Vector2Int, bool>();
        foreach (KeyValuePair<Vector2Int, Tile> pair in tiles)
        {
            visible.Add(pair.Key, false);
        }

        visible[center] = true;


        Vector2Int up = new Vector2Int(0, 1);
        Vector2Int down = new Vector2Int(0, -1);
        Vector2Int left = new Vector2Int(-1, 0);
        Vector2Int right = new Vector2Int(1, 0);
        Vector2Int upright = up + right;
        Vector2Int upleft = up + left;
        Vector2Int downright = down + right;
        Vector2Int downleft = down + left;
        Vector2Int[] directions = { up, down, left, right, upright, upleft, downright, downleft };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int current = center;
            Vector2Int next = current + dir;
            while (tiles.ContainsKey(next) && tiles[next] != null && tiles[next].Type.IsWalkable)
            {
                visible[next] = true;
                current = next;
                next = next + dir;
            }
        }

        bool changeFound = true;
        while (changeFound)
        {
            changeFound = false;
            foreach (KeyValuePair<Vector2Int, Tile> pair in tiles)
            {
                if (pair.Value != null && pair.Value.Type.IsWalkable && visible[pair.Key] == false)
                {
                    Vector2Int nup = pair.Key + up;
                    Vector2Int ndown = pair.Key + down;
                    Vector2Int nleft = pair.Key + left;
                    Vector2Int nright = pair.Key + right;
                    Vector2Int nupleft = pair.Key + upleft;
                    Vector2Int nupright = pair.Key + upright;
                    Vector2Int ndownright = pair.Key + downright;
                    Vector2Int ndownleft = pair.Key + downleft;

                    if (pair.Key.x < center.x && pair.Key.y < center.y)
                    {
                        if ((IsVisible(nupright, tiles, visible) && IsVisible(nup, tiles, visible) && IsVisible(nleft, tiles, visible)) ||
                            (IsVisible(nup, tiles, visible) && IsVisible(nupright, tiles, visible) && IsVisible(nright, tiles, visible)))
                        {
                            visible[pair.Key] = true;
                            changeFound = true;
                        }
                    }

                    if (pair.Key.x > center.x && pair.Key.y < center.y)
                    {
                        if ((IsVisible(nupleft, tiles, visible) && IsVisible(nup, tiles, visible) && IsVisible(nright, tiles, visible)) ||
                            (IsVisible(nup, tiles, visible) && IsVisible(nupleft, tiles, visible) && IsVisible(nleft, tiles, visible)))
                        {
                            visible[pair.Key] = true;
                            changeFound = true;
                        }
                    }

                    if (pair.Key.x < center.x && pair.Key.y > center.y)
                    {
                        if ((IsVisible(ndownright, tiles, visible) && IsVisible(ndown, tiles, visible) && IsVisible(nleft, tiles, visible)) ||
                            (IsVisible(ndown, tiles, visible) && IsVisible(ndownright, tiles, visible) && IsVisible(nright, tiles, visible)))
                        {
                            visible[pair.Key] = true;
                            changeFound = true;
                        }
                    }


                    if (pair.Key.x > center.x && pair.Key.y > center.y)
                    {
                        if ((IsVisible(ndownleft, tiles, visible) && IsVisible(ndown, tiles, visible) && IsVisible(nright, tiles, visible)) ||
                            (IsVisible(ndown, tiles, visible) && IsVisible(ndownleft, tiles, visible) && IsVisible(nleft, tiles, visible)))
                        {
                            visible[pair.Key] = true;
                            changeFound = true;
                        }
                    }

                }
            }
        }

        foreach (KeyValuePair<Vector2Int, Tile> pair in tiles)
        {
            if (visible[pair.Key] == true)
            {
                pair.Value.Behaviour.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    bool IsVisible(Vector2Int pos, Dictionary<Vector2Int, Tile> tiles, Dictionary<Vector2Int, bool> visible)
    {
        return (tiles.ContainsKey(pos) && tiles[pos] != null && tiles[pos].Type.IsWalkable && visible[pos] == true);
    }

    private void OnActionResultNotification(ActionResultNotification notification)
    {
        if (notification.AI == this)
        {
            BaseOnActionResultNotification(notification);
        }
    }

    ~ChaseAI()
    {
        NotificationCenter.RemoveListener<ActionResultNotification>(OnActionResultNotification);
    }

}