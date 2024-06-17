using UnityEngine;

public class SystemCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private SettingPanel _settingPanel;

    private void Start()
    {
        _pausePanel.AddEvent(1, _settingPanel.ShowUI);
    }

    private void OnEsc()
    {
        if (_pausePanel.IsActive)
            _pausePanel.DisableUI();
        else if (_settingPanel.IsActive)
            _settingPanel.DisableUI();
        else
        {
            _pausePanel.ShowUI();
        }
    }

    private void OnUpControl()
    {
        if (_pausePanel.IsActive)
            _pausePanel.ControlUp();
        else if (_settingPanel.IsActive)
        {
            _settingPanel.ControlUp();
        }
    }

    private void OnDownControl()
    {
        if (!_pausePanel.IsActive)
            _pausePanel.ControlDown();
        else if (_settingPanel.IsActive)
        {
            _settingPanel.ControlDown();
        }
    }

    private void OnLeftControl()
    {
        if (_settingPanel.IsActive)
        {
            _settingPanel.ControlLeft();
        }
    }

    private void OnRightControl()
    {
        if (_settingPanel.IsActive)
        {
            _settingPanel.ControlRight();
        }
    }
    
    private void OnSelect()
    {
        if (_pausePanel.IsActive)
            _pausePanel.Select();
        else if (_settingPanel.IsActive)
            _settingPanel.Select();
    }

}
