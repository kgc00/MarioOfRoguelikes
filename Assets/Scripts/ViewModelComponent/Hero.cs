using System;
using System.Collections;
using UnityEngine;

public class Hero : Unit {

    protected override IAction GetAction () {
        if (Input.GetKeyDown (KeyCode.W)) {
            return new Move (new Point (0, 1));
        } else if (Input.GetKeyDown (KeyCode.A)) {
            return new Move (new Point (-1, 0));
        } else if (Input.GetKeyDown (KeyCode.S)) {
            return new Move (new Point (0, -1));
        } else if (Input.GetKeyDown (KeyCode.D)) {
            return new Move (new Point (1, 0));
        }

        return new NoAction ();
    }
}