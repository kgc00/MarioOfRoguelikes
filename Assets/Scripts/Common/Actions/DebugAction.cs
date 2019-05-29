using UnityEngine;

public class DebugAction : Action {
    public DebugAction (BaseAI creator) : base (creator) {
        Cost = 1;
    }

    public override ActionResult Perform (Unit actor) {
        Debug.Log ("Debug Action performed.");
        return success ();
    }

}