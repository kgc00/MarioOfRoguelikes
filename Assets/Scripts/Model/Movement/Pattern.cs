using UnityEngine;

[CreateAssetMenu (fileName = "pattern", menuName = "OurGame/Movement Patterns/pattern", order = 1)]
public class Pattern : ScriptableObject {
    public Vector2Int[] pattern;
}