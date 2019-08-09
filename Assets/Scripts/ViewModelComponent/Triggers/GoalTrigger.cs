class GoalTrigger : BaseTrigger {
    public void OnEnter (Unit unit, Tile tile) {
        if (unit.AI.IsHero ()) {
            unit.energy.Add (4);
            unit.Behaviour.MatchSizeToEnergy (unit.energy.Current / unit.energy.Max);
            unit.ClearStuff ();
            GameManager.Instance.NextLevel ();
        }
    }
    public void OnLeave (Unit unit, Tile tile) { }
    public void StartTimer (Board board, Tile tile) { }
}