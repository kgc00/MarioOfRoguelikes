public class MonsterAI : BaseAI
{
    public bool IsHero()
    {
        return false;
    }

    public Action TakeTurn()
    {
        return new MoveAction(new Vector2Int(1, 0));
    }
}