using System;
using System.Collections;
using UnityEngine;

public class Monster : Unit {
    protected override IAction GetAction () {
        return new WaypointMovement ();
    }
}