using System.Collections;
using DG.Tweening;
using StageManage;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageListSO stageList;
    [SerializeField] private CanvasGroup _backGround;

    private float _fadeDuration = 0.2f;
    
    public void ChangeStage(int id)
    {
        StageSO stage = stageList.FindStage(id);
        if (stage == null)
        {
            Debug.LogError("Stage ID is Not Exist");
            return;
        }

        StartCoroutine(ChangeStageCoroutine());
        GameManager.Instance.levelManager.SetStage(stage);
    }

    private IEnumerator ChangeStageCoroutine()
    {
        _backGround.DOFade(1f, _fadeDuration);
        yield return new WaitForSeconds(0.5f);
        _backGround.DOFade(0f, _fadeDuration);
    }
}