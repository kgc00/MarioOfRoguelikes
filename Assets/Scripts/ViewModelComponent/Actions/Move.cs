using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IAction {

    private Point point;
    public uint GetCost () {
        return 1;
    }

    public Move (Point p) {
        this.point = p;
    }

    public void Perform (GameObject user) {
        Unit unit = user.GetComponent<Unit> ();

        if (point != null) {
            Point newPoint = unit.tile.pos + point;
            unit.MoveToPoint (newPoint);
        }
    }
}