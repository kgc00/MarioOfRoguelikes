using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : IAction {

    public uint GetCost () {
        return 1;
    }

    public void Perform (GameObject user) {
        Waypoints waypoints = user.GetComponent<Waypoints> ();
        Unit unit = user.GetComponent<Unit> ();
        if (waypoints != null && unit != null) {
            Point translation = waypoints.GetNextWaypoint ();
            Point waypoint = unit.tile.pos + translation;
            unit.MoveToPoint (waypoint);
        }
    }
}