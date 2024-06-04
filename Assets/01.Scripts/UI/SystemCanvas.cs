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
        {
            _pausePanel.DisableUI();

        }
        else
        {
            _pausePanel.ShowUI();

        }
    }

    private void OnUpControl()
    {
        if (!_pausePanel.IsActive)
            return;
        _pausePanel.ControlUp();
    }

    private void OnDownControl()
    {
        if (!_pausePanel.IsActive)
            return;
        _pausePanel.ControlDown();
    }
    
    private void OnSelect()
    {
        if (!_pausePanel.IsActive)
            return;
        print("선택됨");
        _pausePanel.Select();
    }

}
