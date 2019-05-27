using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData : ScriptableObject {
    public List<Vector3> tiles;

    [Serializable]
    public struct MonstersSpawnData {
        public Vector3 location;
        public Monster monster;

        public MonstersSpawnData (Vector3 _location, Monster _monster) {
            this.monster = _monster;
            this.location = _location;
        }
    }
    public List<MonstersSpawnData> monsters;
}