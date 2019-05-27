using UnityEngine;

[CreateAssetMenu (fileName = "New Unit", menuName = "OurGame/Unit")]
public class UnitType : ScriptableObject {
    public Sprite Image;
    public Breeds Breed;

    public enum Breeds {
        HERO,
        MONSTER,
    }

}