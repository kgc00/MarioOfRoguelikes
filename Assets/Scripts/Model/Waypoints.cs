using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    public Queue<Point> waypoints = new Queue<Point> ();
    public Pattern pattern;
    private void Awake () {
        if (pattern) {
            GenerateWaypoints (pattern);
        }
    }
    private void GenerateWaypoints (Pattern pattern) {
        Board board = FindObjectOfType<Board> ();
        waypoints = new Queue<Point> (pattern.pattern);
    }

    public Point GetNextWaypoint () {
        Point next = waypoints.Dequeue ();
        waypoints.Enqueue (next);
        return next;
    }
}