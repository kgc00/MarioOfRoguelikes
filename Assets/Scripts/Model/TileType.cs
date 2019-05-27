using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "OurGame/Tile")]
public class TileType : ScriptableObject
{
    public Sprite Image;
    public bool IsWalkable;

    public TileBehaviour Prefab;
}