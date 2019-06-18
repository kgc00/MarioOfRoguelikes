using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{

    private Board board;
    public LevelData LevelData;

    private void Awake()
    {
        board = new Board();
        board.Container = this;
        board.Load();
        Camera.main.gameObject.AddComponent<CameraZoom>().CenterAndZoom();
    }

    private void Update()
    {
        board.Tick();
    }
}