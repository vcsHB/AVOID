using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PausePanel : WindowPanel
{
    [SerializeField] private RectTransform _selectEdge;
    [SerializeField] private Button[] _buttons;
    private float[] _yDeltaPositions = new float[3];

    [Header("Select Setting")]
    [SerializeField] private int _currentSelect;
    [SerializeField] private float _selectMoveDuration = 0.1f;

    private bool _canControl;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _buttons.Length; i++)
        {
            _yDeltaPositions[i] = (_buttons[i].transform as RectTransform).anchoredPosition.y;
        }
    }

    private void Start()
    {
        // CONTINUE 버튼에 이벤트 구독
        AddEvent(0, DisableUI);
        AddEvent(1, DisableUI);
    }

    public void AddEvent(int index, UnityAction callBack)
    {
        _buttons[index].onClick.AddListener(callBack);
    }


    public override void ShowUI()
    {
        if (_isActive) return;
        PlayerManager.Instance.Player.MovementCompo.SetStun(true);
        _isActive = true;
        SetVisible(true);
        _rectTrm.DOAnchorPos(_targetPosition, _onOffTime).SetUpdate(true);
    }

    public override void DisableUI()
    {
        if (!_isActive) return;
        PlayerManager.Instance.Player.MovementCompo.SetStun(false);
        _isActive = false;
        _rectTrm.DOAnchorPos(_defaultPosition, _onOffTime).SetUpdate(true).OnComplete(() => SetVisible(false));

    }       

    public void ControlUp()
    {
        if (_currentSelect <= 0) return;
        _currentSelect--;
        MoveSelect();
    }
    public void ControlDown()
    {
        if (_currentSelect >= 2) return;
        _currentSelect++;
        MoveSelect();
    }

    private void MoveSelect()
    {
        _canControl = false;

        _selectEdge.DOAnchorPosY(_yDeltaPositions[_currentSelect], _selectMoveDuration).SetEase(Ease.InOutExpo).SetUpdate(true).
            OnComplete(() => _canControl = true);
    }

    public void Select()
    {
        _buttons[_currentSelect].onClick?.Invoke();
        
    }
    
}
