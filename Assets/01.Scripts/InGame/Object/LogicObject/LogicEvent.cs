using UnityEngine.Events;

public class LogicEvent
{

    public UnityEvent OnLogicOverEvent;

    public void SolveLogic()
    {
        OnLogicOverEvent?.Invoke();
    }
}