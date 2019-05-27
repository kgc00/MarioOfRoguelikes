using UnityEngine;
public abstract class Stat
{
    public virtual float Current { get; protected set; }
    public virtual float Max { get; protected set; }
    public virtual void Tick() { }
    public void Change(float amount)
    {
        Current += amount;

        if (Current < 0)
        {
            Debug.Log("Error, something reduced stat value below zero.");
        }

        Current = Mathf.Clamp(Current, 0, Max);
    }
}