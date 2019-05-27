using UnityEngine;
[System.Serializable]
public class Hero : Unit
{
    public Hero(Board b, Vector2Int p, UnitType t, GameObject holder) : base(b, p, t, holder) { }

    protected override Action TakeTurn()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return new MoveAction(this, new Vector2Int(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return new MoveAction(this, new Vector2Int(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return new MoveAction(this, new Vector2Int(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return new MoveAction(this, new Vector2Int(1, 0));
        }


        return null;
    }
}