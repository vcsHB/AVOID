using StageManage;
using UnityEngine;
using UnityEngine.Events;

public class StageChecker : MonoBehaviour
{
    [SerializeField] private StageSO _checkStageSO;

    public UnityEvent stageClearEvent;

    private void Start()
    {
        Check();
    }

    public void Check()
    {
        if (GameManager.Instance.stageManager.dataList.CheckClear(_checkStageSO.id))
        {
            stageClearEvent?.Invoke();
        }
    }

}
