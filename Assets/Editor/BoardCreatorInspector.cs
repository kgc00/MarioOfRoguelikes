using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{

    string[] unitTypeNames;
    int spawnUnitIndex = 0;

    string[] tileTypeNames;
    int spawnTileIndex = 0;

    public BoardCreator Current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    // private void Start()
    // {
    //     Current.InputHandler.onKeyPress += HandleKeyPresses;
    // }

    // ~BoardCreatorInspector()
    // {
    //     Current.InputHandler.onKeyPress -= HandleKeyPresses;
    // }

    private void HandleKeyPresses()
    {
        Debug.Log("keycode: ");
    }

    private void OnEnable()
    {
        Current.RefreshUnitTypes();
        Current.RefreshTileTypes();
    }

    public override void OnInspectorGUI()
    {
        unitTypeNames = getUnitNames();
        tileTypeNames = getTileNames();

        DrawDefaultInspector();

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Spawn");
        spawnTileIndex = EditorGUILayout.Popup(spawnTileIndex, tileTypeNames);
        Current.UpdateSelectedTileType(spawnTileIndex);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Spawn");
        spawnUnitIndex = EditorGUILayout.Popup(spawnUnitIndex, unitTypeNames);
        Current.UpdateSelectedUnitType(spawnUnitIndex);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Refresh"))
        {
            Current.RefreshUnitTypes();
            Current.RefreshTileTypes();
        }
        if (GUILayout.Button("Fill Board"))
        {
            Current.FillBoard();
        }
        if (GUILayout.Button("Clear Board"))
        {
            Current.ClearBoard();
        }

        if (GUILayout.Button("Place Tile"))
        {
            Current.PlaceSelectedTile(Current.MarkerPosition);
        }

        if (GUILayout.Button("Delete Tile"))
        {
            Current.DeleteTileAt(Current.MarkerPosition);
        }
        if (GUILayout.Button("Place Unit"))
        {
            Current.PlaceSelectedUnit(Current.MarkerPosition);
        }
        if (GUILayout.Button("Delete Unit"))
        {
            Current.DeleteUnitAt(Current.MarkerPosition);
        }
        if (GUILayout.Button("Save"))
        {
            Current.Save();
        }
        if (GUILayout.Button("Load"))
        {
            Current.Load();
        }
    }

    private string[] getUnitNames()
    {
        List<string> names = new List<string>();
        foreach (UnitType unitType in Current.UnitTypes)
        {
            names.Add(unitType.ToString());
        }
        return names.ToArray();
    }

    private string[] getTileNames()
    {
        List<string> names = new List<string>();
        foreach (TileType tileType in Current.TileTypes)
        {
            names.Add(tileType.ToString());
        }
        return names.ToArray();
    }
}