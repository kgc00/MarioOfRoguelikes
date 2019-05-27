using UnityEngine;

public class MoveAction : Action
{
    public Vector2Int Direction { get; private set; }
    public MoveAction(Unit actor, Vector2Int direction) : base(actor)
    {
        Cost = 1;
        Direction = direction;
    }

    public override ActionResult Perform()
    {
        Vector2Int pos = Actor.Position + Direction;

        // Check if there is actually a tile
        Tile tile = Actor.TheBoard.TileAt(pos);
        if (tile == null)
        {
            return new Failure();
        }

        // Check if there is another unit
        Unit target = Actor.TheBoard.UnitAt(pos);
        if (target != null)
        {
            return new Alternate(new MoveAttack(Actor, target));
        }


        Actor.TheBoard.Move(Actor, pos);

        return new Success();
    }

}