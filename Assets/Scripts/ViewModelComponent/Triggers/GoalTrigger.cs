class GoalTrigger : BaseTrigger
{
    public void OnEnter(Unit unit, Tile tile)
    {
        GameManager.Instance.NextLevel();
    }
    public void OnLeave(Unit unit, Tile tile) { }
    public void StartTimer(Board board, Tile tile) { }
}