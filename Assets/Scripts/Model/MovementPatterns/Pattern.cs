using UnityEngine;

[CreateAssetMenu (fileName = "pattern", menuName = "Movement Patterns/pattern", order = 1)]
public class Pattern : ScriptableObject {
    public Point[] pattern;
}