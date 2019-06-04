using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] TileBehaviour tileViewPrefab;
    [SerializeField] UnitBehaviour unitViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;
    public Vector2Int MarkerPosition { get; private set; }
    Transform marker;
    string fileName = "boardcreator";
    public EditorInputHandler InputHandler;
    [HideInInspector] public List<UnitType> UnitTypes = new List<UnitType>();
    [HideInInspector] public List<TileType> TileTypes = new List<TileType>();
    public int SelectedTileTypeIndex { get; private set; }
    public int SelectedUnitTypeIndex { get; private set; }

    [SerializeField] int width = 10; // world space x
    [SerializeField] int depth = 10; // world space y

    [SerializeField] LevelData levelData;
    private Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();
    private Dictionary<Vector2Int, Unit> units = new Dictionary<Vector2Int, Unit>();

    private void Awake()
    {
        GameObject instance = Instantiate(
            tileSelectionIndicatorPrefab, transform
        ) as GameObject;
        marker = instance.transform;
        InputHandler = gameObject.AddComponent<EditorInputHandler>();
        InputHandler.Initialize(this);
    }

    public void SetFileName(string s)
    {
        if (s != null && s.Length > 0)
        {
            fileName = s;
        }
    }

    public void FillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                PlaceSelectedTile(new Vector2Int(i, j));
            }
        }
    }

    public void ClearBoard()
    {
        List<Vector2Int> tilePositions = new List<Vector2Int>(tiles.Keys);
        foreach (Vector2Int pos in tilePositions)
        {
            DeleteTileAt(pos);
        }

        List<Vector2Int> unitPositions = new List<Vector2Int>(units.Keys);
        foreach (Vector2Int pos in unitPositions)
        {
            DeleteUnitAt(pos);
        }
    }

    public void PlaceSelectedTile(Vector2Int p)
    {
        TileType tileType = TileTypes[SelectedTileTypeIndex];
        PlaceTile(p, tileType);
    }

    public void PlaceTile(Vector2Int p, TileType tileType)
    {
        if (tiles.ContainsKey(p))
            DeleteTileAt(p);

        Tile tile = BoardHelper.CreateTile(
            transform, null, p, tileType
        );

        // Put tile in the dictionary
        tiles.Add(tile.Position, tile);
    }

    public void PlaceSelectedUnit(Vector2Int p)
    {
        UnitType unitType = UnitTypes[SelectedUnitTypeIndex];

        PlaceUnit(p, unitType);
    }

    public void PlaceUnit(Vector2Int p, UnitType unitType)
    {
        if (units.ContainsKey(p))
            DeleteUnitAt(p);

        Unit unit = BoardHelper.CreateUnit(transform, null, p, unitType);

        units.Add(unit.Position, unit);
    }
    public void DeleteUnitAt(Vector2Int p)
    {
        BoardHelper.DeleteUnitAt(p, ref units);
    }

    public void DeleteTileAt(Vector2Int p)
    {
        BoardHelper.DeleteTileAt(p, ref tiles);
    }

    public void RefreshUnitTypes()
    {
        UnitTypes.Clear();
        Object[] tmp = Resources.LoadAll("Units", typeof(UnitType));
        for (int i = 0; i < tmp.Length; ++i)
        {
            UnitTypes.Add((UnitType)tmp[i]);
        }
    }

    public void RefreshTileTypes()
    {
        TileTypes.Clear();
        Object[] tmp = Resources.LoadAll("Tiles", typeof(TileType));
        for (int i = 0; i < tmp.Length; ++i)
        {
            TileTypes.Add((TileType)tmp[i]);
        }
    }

    public void MoveAndUpdateMarker(Vector2Int direction)
    {
        MarkerPosition += direction;
        marker.position = new Vector3(MarkerPosition.x, MarkerPosition.y, -2);
    }

    public void UpdateSelectedTileType(int index)
    {
        SelectedTileTypeIndex = index;
    }
    public void UpdateSelectedUnitType(int index)
    {
        SelectedUnitTypeIndex = index;
    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();

        board.tiles = new List<TileSpawnData>();
        foreach (
            KeyValuePair<Vector2Int, Tile> element in tiles)
            board.tiles.Add(new TileSpawnData(element.Key, element.Value.Type));

        board.units = new List<UnitSpawnData>();
        foreach (KeyValuePair<Vector2Int, Unit> element in units)
            board.units.Add(new UnitSpawnData(
                element.Key, element.Value.Type));

        string fileURI = string.Format(
            "Assets/Resources/Levels/{1}.asset",
            filePath, fileName);
        AssetDatabase.CreateAsset(board, fileURI);
    }
    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }
    public void Load()
    {
        ClearBoard();
        if (levelData == null)
            return;

        foreach (TileSpawnData data in levelData.tiles)
        {
            PlaceTile(data.location, data.tileType);
        }

        foreach (UnitSpawnData data in levelData.units)
        {
            PlaceUnit(data.location, data.unitType);
        }
    }
}