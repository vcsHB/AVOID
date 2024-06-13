using System;
using System.Collections;
using DG.Tweening;
using StageManage;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageListSO stageList;
    public StageDataList dataList { get; private set; }

    [SerializeField] private CanvasGroup _backGround;


    private float _fadeDuration = 0.2f;

    private void Awake()
    {
        dataList = DBManager.GetStageData();
    }

    public void ChangeStage(int id)
    {
        StageSO stage = stageList.FindStage(id);
        if (stage == null)
        {
            Debug.LogError("Stage ID is Not Exist");
            return;
        }
        if(id != 0) // 스테이지 선택 스테이지만 제외
            dataList.currentPlayedStageId = id;
        StartCoroutine(ChangeStageCoroutine());
        GameManager.Instance.levelManager.SetStage(stage);
        DBManager.SaveStageData(dataList);
    }

    private IEnumerator ChangeStageCoroutine()
    {
        _backGround.DOFade(1f, _fadeDuration);
        yield return new WaitForSeconds(0.5f);
        _backGround.DOFade(0f, _fadeDuration);
    }

    public void ClearStageByPortal(int id)
    {
        int beforeId = GameManager.Instance.levelManager.CurrentStage.id;
        if(beforeId != 0) // 스테이지 선택 레벨 제외
            dataList.Clear(beforeId);
        ChangeStage(id);
    }


}