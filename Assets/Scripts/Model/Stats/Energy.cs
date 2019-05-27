using UnityEngine;

public class Energy : Stat
{
    public float Bars
    {
        get
        {
            return Mathf.Floor(Current / BarSize);
        }
    }
    public float Rate { get; protected set; }
    public float BarSize { get; protected set; }

    public void Spend(float bars)
    {
        ChangeAmount(-bars * BarSize);
    }

    public Energy()
    {
        current = 30f;
        Max = 30;
        Rate = 10f;
        BarSize = 1;
    }

    public override void Tick()
    {
        regenerate();
    }

    private void regenerate()
    {
        ChangeAmount(Rate * Time.deltaTime);
    }




}