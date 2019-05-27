using UnityEngine;
[System.Serializable]
public class Monster : Unit
{
    public Monster(Board b, Vector2Int p, UnitType t, GameObject holder) : base(b, p, t, holder) { }

    protected override Action TakeTurn()
    {
        return new MoveAction(this, new Vector2Int(1, 0));
    }
}