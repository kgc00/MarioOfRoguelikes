using UnityEngine;

public class DestroySelf : Action
{
    public DestroySelf(BaseAI creator) : base(creator)
    {
        Cost = 0;
    }

    public override ActionResult Perform(Unit actor)
    {
        actor.Board.DeleteUnitAt(actor.Position);
        return success();
    }

}