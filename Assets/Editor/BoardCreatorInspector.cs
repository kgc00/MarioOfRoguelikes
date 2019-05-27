using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{

    string[] unitNames;
    int spawnUnitIndex = 0;

    public BoardCreator Current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        unitNames = getUnitNames();

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Spawn");
        spawnUnitIndex = EditorGUILayout.Popup(spawnUnitIndex, unitNames);
        GUILayout.EndHorizontal();


        if (GUILayout.Button("RefreshUnits"))
            Current.RefreshUnits();

    }

    private string[] getUnitNames()
    {
        List<string> names = new List<string>();
        foreach (UnitType unit in Current.Units)
        {
            names.Add(unit.Name);
        }
        return names.ToArray();
    }
}
