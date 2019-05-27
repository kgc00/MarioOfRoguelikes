using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    private Board board;
    public LevelData levelData;

    [SerializeField] private TileBehaviour tilePrefab;
    public TileBehaviour TilePrefab {
        get {
            return tilePrefab;
        }
    }

    [SerializeField] private UnitBehaviour unitPrefab;
    public UnitBehaviour UnitPrefab {
        get {
            return unitPrefab;
        }
    }

    private void Awake () {
        board = new Board ();
        board.Container = this;
        board.Load ();
    }
}