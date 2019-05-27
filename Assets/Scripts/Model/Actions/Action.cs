public abstract class Action
{
    public Action(Unit actor)
    {
        Actor = actor;
        Cost = 0;
    }
    public Unit Actor { get; private set; }
    public float Cost { get; protected set; }
    public abstract ActionResult Perform();
}

public abstract class ActionResult
{
    public ActionResultType Type { get; protected set; }
    public Action AlternateAction { get; protected set; }
}

public enum ActionResultType
{
    SUCCESS,
    FAILURE,
    ALTERNATE
}

public class Alternate : ActionResult
{
    public Alternate(Action action)
    {
        Type = ActionResultType.ALTERNATE;
        AlternateAction = action;
    }
}

public class Success : ActionResult
{
    public Success()
    {
        Type = ActionResultType.SUCCESS;
        AlternateAction = null;
    }
}

public class Failure : ActionResult
{
    public Failure()
    {
        Type = ActionResultType.FAILURE;
        AlternateAction = null;
    }
}
