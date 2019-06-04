public abstract class Action
{

    protected BaseAI Creator;
    public Action(BaseAI creator)
    {
        Cost = 0;
        Creator = creator;
    }
    public Unit Actor { get; private set; }
    public float Cost { get; protected set; }
    public abstract ActionResult Perform(Unit actor);

    protected ActionResult failure()
    {
        ActionResult result = new Failure();
        NotificationCenter.FireEvent<ActionResultNotification>(new ActionResultNotification(result, Creator));
        return result;
    }

    protected ActionResult success()
    {
        ActionResult result = new Success();
        NotificationCenter.FireEvent<ActionResultNotification>(new ActionResultNotification(result, Creator));
        return result;
    }

    protected ActionResult alternate(Action alternateAction)
    {
        ActionResult result = new Alternate(alternateAction);
        NotificationCenter.FireEvent<ActionResultNotification>(new ActionResultNotification(result, Creator));
        return result;
    }
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