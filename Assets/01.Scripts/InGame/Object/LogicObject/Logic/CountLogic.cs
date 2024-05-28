using UnityEngine;

[CreateAssetMenu(menuName = "SO/Logic/CountLogic")]
[System.Serializable]
public class CountLogic : Logic
{
    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int goalCount = 1;
    public int GoalCount => goalCount;
    protected override void TriggerLogic()
    {
        count++;
        if (count >= goalCount)
        {
            isActive = true;
        }
    }
}