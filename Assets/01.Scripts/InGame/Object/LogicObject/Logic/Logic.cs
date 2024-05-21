[System.Serializable]
public abstract class Logic
{
    public LogicType logicType;
    public bool isActive;

    public void Trigger()
    {
        if (isActive) return;
        TriggerLogic();
    }

    protected abstract void TriggerLogic();
}