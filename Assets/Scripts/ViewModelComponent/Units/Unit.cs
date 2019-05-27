using UnityEngine;

[System.Serializable]
public class Unit
{
    public GameObject Holder { get; protected set; }
    public UnitType Type { get; protected set; }
    public Vector2Int Position { get; set; }
    private Energy energy = new Energy();
    public Board TheBoard { get; protected set; }

    public Unit(Board b, Vector2Int p, UnitType t, GameObject holder)
    {
        Position = p;
        Type = t;
        Holder = holder;
        TheBoard = b;
    }

    public void Tick()
    {
        if (TheBoard == null)
        {
            return;
        }
        energy.Tick();

        Action action = TakeTurn();

        if (action != null && action.Cost <= energy.Bars)
        {
            ActionResult result;
            do
            {
                result = action.Perform();
                if (result.Type == ActionResultType.ALTERNATE)
                {
                    action = result.AlternateAction;
                }
            } while (result.Type == ActionResultType.ALTERNATE);

            if (result.Type == ActionResultType.SUCCESS)
            {

                energy.Spend(action.Cost);
            }
        }
        NotificationCenter.FireEvent<StatChange>(new StatChange(this, typeof(Energy), energy.Bars));
    }

    protected virtual Action TakeTurn() { return null; }
}