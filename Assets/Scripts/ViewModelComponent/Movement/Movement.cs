using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Unit owner;
    private Queue<Tile> waypoints = new Queue<Tile> ();

    private void Awake () {
        owner = GetComponent<Unit> ();
        GenerateWaypoints ();
    }

    private void GenerateWaypoints () {
        Board board = FindObjectOfType<Board> ();
        waypoints.Enqueue (board.GetTile (new Point (2, 5)));
        waypoints.Enqueue (board.GetTile (new Point (3, 5)));
        waypoints.Enqueue (board.GetTile (new Point (4, 5)));
        waypoints.Enqueue (board.GetTile (new Point (3, 5)));
    }

    private void Move (Tile t) {
        owner.Place (t);
        owner.Match ();
    }

    private Tile GetNextWaypoint () {
        Tile next = waypoints.Dequeue ();
        waypoints.Enqueue (next);
        return next;
    }

    public void Perform () {
        Tile waypoint = GetNextWaypoint ();
        Move (waypoint);
    }
}