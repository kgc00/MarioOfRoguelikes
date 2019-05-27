using UnityEngine;
public abstract class Stat
{
    protected float current;
    public float Current
    {
        get
        {
            return current;
        }
    }

    public void ChangeAmount(float amount)
    {
        current = Mathf.Clamp(current + amount, 0, Max);
    }

    public virtual float Max { get; protected set; }
    public virtual void Tick() { }

}