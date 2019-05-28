using UnityEngine;

public class MoveAction : Action {
    public Vector2Int Direction { get; private set; }
    public MoveAction (Vector2Int direction, BaseAI creator) : base (creator) {
        Cost = 1;
        Direction = direction;
    }

    public override ActionResult Perform (Unit actor) {
        Vector2Int pos = actor.Position + Direction;

        Tile tile = actor.Board.TileAt (pos);
        if (tile == null || !tile.Type.IsWalkable) {
            return failure ();
        }

        // Check if there is another unit
        Unit target = actor.Board.UnitAt (pos);
        if (target != null) {
            // return alternate()
            return alternate (new MoveAttack (target, Creator));
        }

        actor.Board.Move (actor, pos);

        return success ();
    }

}