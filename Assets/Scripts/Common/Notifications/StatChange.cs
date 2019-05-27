public class StatChange : Notification
{
    public Unit Actor { get; private set; }
    public System.Type Type { get; private set; }
    public float NewValue { get; private set; }
    public StatChange(Unit actor, System.Type type, float newValue)
    {
        Actor = actor;
        Type = type;
        NewValue = newValue;
    }
}