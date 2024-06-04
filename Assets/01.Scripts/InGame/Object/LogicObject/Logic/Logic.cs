using Unity.VisualScripting;
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

    public void SetActive(bool value)
    {
        isActive = value;
    }

    protected abstract void TriggerLogic();
}