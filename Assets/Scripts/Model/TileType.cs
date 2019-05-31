using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "OurGame/Tile")]
public class TileType : ScriptableObject
{
    public Sprite Image;
    public bool IsWalkable;

    public TriggerTypes TriggerType;
    public EnergyRefillTriggerData energyRefillTriggerData;
    public SpikeTrapTriggerData spikeTrapTriggerData;
    public SwitchTriggerData switchTriggerData;
    public TileBehaviour Prefab;

    public enum TriggerTypes
    {
        NONE,
        ENERGY_REFILL,
        GOAL,
        SPIKE_TRAP,
        SWITCH,
    }
}