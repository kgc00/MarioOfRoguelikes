class EnergyFillTrigger : BaseTrigger {
    EnergyRefillTriggerData data;
    int remainingUses;
    int refreshTime;
    int refillAmount;
    public EnergyFillTrigger (EnergyRefillTriggerData data) {
        this.remainingUses = data.Uses;
        this.refreshTime = data.RefreshTime;
        this.refillAmount = data.RefillAmount;
    }

    public void OnEnter (Unit unit, Tile tile) {
        unit.energy.Add (refillAmount);
    }
    public void OnLeave (Unit unit, Tile tile) { }
}