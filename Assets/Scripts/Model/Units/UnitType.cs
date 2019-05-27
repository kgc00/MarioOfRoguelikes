using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "OurGame/Unit")]
public class UnitType : ScriptableObject
{
    public Sprite Image;
    public Breeds Breed;
    public string Name;

    public enum Breeds
    {
        HERO,
        MONSTER,
    }

}