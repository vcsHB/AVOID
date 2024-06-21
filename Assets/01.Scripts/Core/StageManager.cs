using System;
using System.Collections;
using DG.Tweening;
using StageManage;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public StageListSO stageList;
    public StageDataList dataList { get; private set; }
    [SerializeField] private CanvasGroup _backGround;
    public int moveCount;
    [SerializeField] private bool _isInGameScene = true;
    private float _fadeDuration = 0.2f;

    private void Awake()
    {
        dataList = DBManager.GetStageData();
    }

    private void Start()
    {
        if(_isInGameScene)
            PlayerManager.Instance.Player.PlayerMovementCompo.OnMovementEvent += HandleMovementEvent;
    }

    private void HandleMovementEvent()
    {
        moveCount++; 
    }

    public void ChangeStage(int id)
    {
        moveCount = 0;
        StageSO stage = stageList.FindStage(id);
        if (stage == null)
        {
            Debug.LogError("Stage ID is Not Exist");
            return;
        }
        if(id != 0) // 스테이지 선택 스테이지만 제외
            dataList.currentPlayedStageId = id;
        StartCoroutine(ChangeStageCoroutine());
        LevelManager.Instance.SetStage(stage);
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
        int beforeId = LevelManager.Instance.CurrentStage.id;
        if(beforeId != 0) // 스테이지 선택 레벨 제외
            dataList.Clear(beforeId, moveCount);
        PlayerSkillManager.Instance.GetSkill<PlayerMoveCountSkill>().DisableSkill();
        ChangeStage(id);
    }


}