using UnityEngine;

public class MoveAttack : Action
{
    public Unit Target { get; private set; }
    public MoveAttack(Unit target, BaseAI creator) : base(creator)
    {
        // setting to -6 to refill unit's energy
        // in case of successful attack
        Cost = -6;
        Target = target;
    }

    public override ActionResult Perform(Unit actor)
    {
        actor.Board.DeleteUnitAt(Target.Position);
        actor.Board.Move(actor, Target.Position);
        return success();
    }

}