using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private SettingPanel _settingPanel;

    private void OnEsc()
    {
        if (_pausePanel.IsActive)
            _pausePanel.DisableUI();
        if (_settingPanel.IsActive)
            _settingPanel.DisableUI();
        else
        {
            _pausePanel.ShowUI();
            PlayerManager.Instance.Player.MovementCompo.SetStun(true);
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
            return;
        _pausePanel.ControlDown();
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
