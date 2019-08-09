using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    private Board board;
    public LevelData LevelData;

    private void Awake () {
        board = new Board ();
        board.Container = this;
        board.Load ();
        Camera.main.gameObject.AddComponent<CameraZoom> ().CenterAndZoom ();
        Camera.main.backgroundColor = new Color (0.1886792f, 0.1886792f, 0.1886792f, 1);
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Instantiate (Resources.Load ("Prefabs/Canvas", typeof (GameObject)));
    }

    private void Update () {
        board.Tick ();
    }
}