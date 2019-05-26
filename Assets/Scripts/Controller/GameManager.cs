using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LevelData level;
    private Board board;
    [SerializeField]
    private Unit monster;
    [SerializeField]
    private Unit hero;
    private void Start () {

        board = FindObjectOfType<Board> ();
        board.Load (level);

        CreateMonster (new Point (5, 5));
        CreateHero (new Point (5, 8));
    }

    private void CreateHero (Point p) {
        Unit test = Instantiate (hero);
        test.Place (board.GetTile (p));
        test.Match ();
    }
    private void CreateMonster (Point p) {
        Unit enemy = Instantiate (monster);
        enemy.Place (board.GetTile (p));
        enemy.Match ();
    }
}