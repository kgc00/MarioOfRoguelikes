using UnityEngine;

public class DebugAction : Action
{
    public DebugAction(Unit actor) : base(actor)
    {
        Cost = 1;
    }

    public override ActionResult Perform()
    {
        Debug.Log("Debug Action performed.");
        return new Success();
    }

}