using UnityEngine.Events;

[System.Serializable]
public class LogicEvent
{

    public UnityEvent OnLogicOverEvent;

    public void SolveLogic()
    {
        OnLogicOverEvent?.Invoke();
    }
}