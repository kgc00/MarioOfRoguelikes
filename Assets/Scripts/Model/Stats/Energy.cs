using UnityEngine;

public class Energy : Stat
{
    public float Bars
    {
        get
        {
            return Mathf.Floor(Current / BarSize);
        }
        set
        {
            Current = value * BarSize;
        }
    }
    public float Rate { get; protected set; }
    public float BarSize { get; protected set; }

    public Energy()
    {
        Current = 30;
        Max = 30;
        Rate = 5f;
        BarSize = 10;
    }

    public override void Tick()
    {
        regenerate();
    }

    private void regenerate()
    {
        Current += Rate * Time.deltaTime;
    }




}