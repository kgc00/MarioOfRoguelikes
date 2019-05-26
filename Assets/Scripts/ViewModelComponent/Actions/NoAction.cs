using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAction : IAction {
    public void Perform (GameObject user) {

    }

    public uint GetCost () {
        return 0;
    }
}