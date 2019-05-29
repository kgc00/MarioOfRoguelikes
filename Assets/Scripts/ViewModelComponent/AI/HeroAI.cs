using UnityEngine;
public class HeroAI : BaseAI {
    public bool IsHero () {
        return true;
    }

    public Action TakeTurn () {
        if (Input.GetKeyDown (KeyCode.W)) {
            return new MoveAction (new Vector2Int (0, 1), this);
        } else if (Input.GetKeyDown (KeyCode.A)) {
            return new MoveAction (new Vector2Int (-1, 0), this);
        } else if (Input.GetKeyDown (KeyCode.S)) {
            return new MoveAction (new Vector2Int (0, -1), this);
        } else if (Input.GetKeyDown (KeyCode.D)) {
            return new MoveAction (new Vector2Int (1, 0), this);
        }

        return null;
    }
}