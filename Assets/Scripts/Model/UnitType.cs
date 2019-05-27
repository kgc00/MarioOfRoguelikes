using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "OurGame/Unit")]
public class UnitType : ScriptableObject
{
    public Sprite Image;
    public AIs AI;

    public UnitBehaviour Prefab;

    public enum AIs
    {
        HERO,
        MONSTER,
    }
}