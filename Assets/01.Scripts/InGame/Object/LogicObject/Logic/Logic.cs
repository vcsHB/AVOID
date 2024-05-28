using UnityEngine;

[System.Serializable]
public abstract class Logic : ScriptableObject
{
    public string logicName;
    public LogicType logicType;
    public bool isActive;

    public void Trigger()
    {
        if (isActive) return;
        TriggerLogic();
    }

    protected abstract void TriggerLogic();
}