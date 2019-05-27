using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public BoardBehaviour Container { get; set; }

    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    private Dictionary<Vector2Int, Unit> units = new Dictionary<Vector2Int, Unit>();

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
        units.Remove(unit.Position);
        unit.Position = pos;
        units.Add(unit.Position, unit);
    }

    public void KillAt(Vector2Int pos)
    {
        Unit unit = UnitAt(pos);
        if (unit != null)
        {
            Object.Destroy(unit.Holder);
        }
        units.Remove(unit.Position);
    }

    public void Load()
    {
        for (int x = 0; x < 10; ++x)
        {
            for (int y = 0; y < 10; ++y)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Tile tile = new Tile(pos, Resources.Load<TileType>("Tiles/Ground"));
                TileBehaviour tileBehaviour = Object.Instantiate<TileBehaviour>(Container.TilePrefab, Container.transform);
                tileBehaviour.SetTile(tile);
                tiles.Add(tile.Position, tile);
            }
        }

        Vector2Int pos1 = new Vector2Int(4, 3);
        UnitType monsterType = Resources.Load<UnitType>("Units/Monster2");

        UnitBehaviour unitBehaviour = Object.Instantiate<UnitBehaviour>(Container.UnitPrefab, Container.transform);
        Monster unit = new Monster(this, pos1, monsterType, unitBehaviour.gameObject);
        unitBehaviour.SetUnit(unit);
        units.Add(unit.Position, unit);

        Vector2Int pos2 = new Vector2Int(7, 2);
        UnitType heroType = Resources.Load<UnitType>("Units/Hero");

        UnitBehaviour heroBehaviour = Object.Instantiate<UnitBehaviour>(Container.UnitPrefab, Container.transform);
        Hero hero = new Hero(this, pos2, heroType, heroBehaviour.gameObject);
        heroBehaviour.SetUnit(hero);
        units.Add(hero.Position, hero);

    }
}