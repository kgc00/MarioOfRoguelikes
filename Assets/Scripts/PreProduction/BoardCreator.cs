
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{

    public List<UnitType> Units = new List<UnitType>();

    public void RefreshUnits()
    {
        Units.Clear();
        Object[] tmp = Resources.LoadAll("Units", typeof(UnitType));
        for (int i = 0; i < tmp.Length; ++i)
        {
            Units.Add((UnitType)tmp[i]);
        }
    }
}