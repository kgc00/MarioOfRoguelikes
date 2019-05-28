public class ChaseAI : BaseAI
{
    private Board board;

    public ChaseAI(Board board)
    {
        this.board = board;
    }

    public bool IsHero()
    {
        return false;
    }

    public Action TakeTurn()
    {
        return new MoveAction(new Vector2Int(1, 0), this);
    }
}