public class ActionResultNotification : Notification {
    public ActionResult Result { get; private set; }
    public BaseAI AI { get; private set; }

    public ActionResultNotification (ActionResult result, BaseAI ai) {
        Result = result;
        AI = ai;
    }
}