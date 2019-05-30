using UnityEngine;

[CreateAssetMenu (fileName = "EnergyValues", menuName = "OurGame/Energy Values", order = 1)]
public class EnergyValues : ScriptableObject {
    public float current, max, rate, barSize;
}