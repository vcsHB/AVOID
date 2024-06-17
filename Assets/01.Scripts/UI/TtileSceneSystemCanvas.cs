using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TtileSceneSystemCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private TitleSceneSettingPanel _settingPanel;


    private void OnEsc()
    {
        if (_settingPanel.IsActive)
            _settingPanel.DisableUI();
        
    }

    private void OnUpControl()
    {
        if (_settingPanel.IsActive)
        {
            _settingPanel.ControlUp();
        }
    }

    private void OnDownControl()
    {
        if (_settingPanel.IsActive)
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
       if (_settingPanel.IsActive)
            _settingPanel.Select();
    }

}
