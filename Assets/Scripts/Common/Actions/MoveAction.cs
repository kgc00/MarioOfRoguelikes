using UnityEngine;

public class MoveAction : Action
{
    public Vector2Int Direction { get; private set; }
    public MoveAction(Vector2Int direction)
    {
        Cost = 1;
        Direction = direction;
    }

    public override ActionResult Perform(Unit actor)
    {
        Vector2Int pos = actor.Position + Direction;

        // Check if there is actually a tile
        Tile tile = actor.Board.TileAt(pos);
        if (tile == null || !tile.Type.IsWalkable)
        {
            return new Failure();
        }


        // Check if there is another unit
        Unit target = actor.Board.UnitAt(pos);
        if (target != null)
        {
            return new Alternate(new MoveAttack(target));
        }


        actor.Board.Move(actor, pos);

        return new Success();
    }

}