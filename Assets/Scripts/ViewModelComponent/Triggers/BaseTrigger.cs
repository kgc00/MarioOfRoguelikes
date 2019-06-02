public interface BaseTrigger
{
    void OnEnter(Unit unit, Tile tile);
    void OnLeave(Unit unit, Tile tile);

    void StartTimer(Board board, Tile tile);
}