using UnityEngine;

[System.Serializable]
public class CountLogic : Logic
{
    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int goalCount = 1;
    
    protected override void TriggerLogic()
    {
        count++;
        if (count >= goalCount)
        {
            isActive = true;
        }
    }
}