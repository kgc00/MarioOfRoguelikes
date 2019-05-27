using UnityEngine;

public class MoveAttack : Action
{
    public Unit Target { get; private set; }
    public MoveAttack(Unit target)
    {
        Cost = 1;
        Target = target;
    }

    public override ActionResult Perform(Unit actor)
    {
        actor.Board.DeleteUnitAt(Target.Position);
        actor.Board.Move(actor, Target.Position);
        return new Success();
    }

}