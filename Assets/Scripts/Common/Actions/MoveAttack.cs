using UnityEngine;

public class MoveAttack : Action {
    public Unit Target { get; private set; }
    public MoveAttack (Unit actor, Unit target) : base (actor) {
        Cost = 1;
        Target = target;
    }

    public override ActionResult Perform () {
        Actor.TheBoard.DeleteUnitAt (Target.Position);
        Actor.TheBoard.Move (Actor, Target.Position);
        return new Success ();
    }

}