using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneSettingPanel : WindowPanel
{
    [SerializeField] private RectTransform _selectBoxTrm;
    [SerializeField] private RectTransform[] _selectOptions;
    
    public Slider _BGMSlider;
    public Slider _SFXSlider;
    public Slider _timeSceleSlider;

    // public Action<float> OnBGMSliderChanged;
    // public Action<float> OnSFXSliderChanged;
    // public Action<float> OnTimeScaleSliderChanged;

    [Header("Select Setting")]
    [SerializeField] private float _selectMoveDuration = 0.1f;
    [SerializeField] private int _currentSelect;
    private Button _exitBtn;
    
    private bool _canControl;

    protected override void Awake()
    {
        base.Awake();
        _exitBtn = _selectOptions[0].Find("BtnPanel").GetComponent<Button>();
    }

    private void Start()
    {
        _exitBtn.onClick.AddListener(Exit);
        //_BGMSlider.onValueChanged.AddListener(HandleBGMSliderValueChanged);

    }

    

    public override void ShowUI()
    {
        if (_isActive) return;
        _isActive = true;
        TitleSceneManager.Instance.canControl = false;
        SetVisible(true);
        _rectTrm.DOAnchorPos(_targetPosition, _onOffTime).SetUpdate(true);;
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

    public void ControlLeft()
    {
        if (_currentSelect <= 0) return;
        _currentSelect--;
        MoveSelect();
    }

    public void ControlRight()
    {
        if (_currentSelect >= 2) return;
        _currentSelect++;
        MoveSelect();
    }

    public void ControlUp()
    {
        if (_currentSelect == 0) return;

        switch (_currentSelect)
        {
            case 1:
                _BGMSlider.value += 2;
                break;
            
            case 2:
                _SFXSlider.value += 2;
                break;
        }
    }
    
    public void ControlDown()
    {
        if (_currentSelect == 0) return;
        switch (_currentSelect)
        {
            case 1:
                _BGMSlider.value -= 2;
                break;
            
            case 2:
                _SFXSlider.value -= 2;
                break;
        }
    }
    
    private void MoveSelect()
    {
        _canControl = false;

        _selectBoxTrm.DOSizeDelta(_selectOptions[_currentSelect].sizeDelta, _selectMoveDuration).SetUpdate(true);
        _selectBoxTrm.DOAnchorPos(_selectOptions[_currentSelect].anchoredPosition, _selectMoveDuration).SetEase(Ease.InOutExpo).SetUpdate(true).
            OnComplete(() => _canControl = true);
    }

    public void Select()
    {
        if (_currentSelect == 0)
        {
            // 0번 : Exit 버튼이 선택 상태라면 나가기
            Exit();
        }
    }

    

    public void Exit()
    {
        DisableUI();
    }
}
