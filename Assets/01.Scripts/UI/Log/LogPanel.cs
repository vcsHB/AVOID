using System;
using DG.Tweening;
using StageManage;
using TMPro;
using UnityEngine;

public class LogPanel : WindowPanel
{
    [SerializeField] private LogSlotUI _slotPrefab;
    [SerializeField] private Transform _contentTrm;
    [SerializeField] private TextMeshProUGUI _logEmptyText;
    [Header("Log Setting")] 
    [SerializeField] private float _spacing = 20f;

    [SerializeField] private float _upPadding = 120f;
    private StageDataList _dataList;
    private float _height;

    protected override void Awake()
    {
        base.Awake();
        _dataList = DBManager.GetStageData();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void ShowUI()
    { 
        if (_isActive) return;
        _isActive = true;
        TitleSceneManager.Instance.canControl = false;
        SetVisible(true);
        _rectTrm.DOAnchorPos(_targetPosition, _onOffTime).SetUpdate(true);
    }
    

    public override void DisableUI()
    {
        if (!_isActive) return;
        
        _rectTrm.DOAnchorPos(_defaultPosition, _onOffTime).SetUpdate(true).OnComplete(() =>
        {
            SetVisible(false);
            TitleSceneManager.Instance.canControl = true;
            _isActive = false;
        });
    }

    [ContextMenu("DebugRefresh")]
    public void HandleRefreshLog()
    {
        _logEmptyText.enabled = _dataList.stageDataList.Count == 0;
        
        for (int i = 0; i < _dataList.stageDataList.Count; i++)
        {
            StageData data = _dataList.stageDataList[i];
            LogSlotUI slot = Instantiate(_slotPrefab, _contentTrm);
            slot.Initialize(data);
            
            // Ydelta를 계산하여 간격두어 배치하기 구현해야됨
            Vector2 offset = new Vector2(0,
                -(_upPadding + (_spacing + slot.height) * i));
            slot.rectTrm.anchoredPosition = offset;
        }
    }
}
