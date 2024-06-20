using UnityEngine;

public class TtileSceneSystemCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private TitleSceneSettingPanel _settingPanel;
    [SerializeField] private LogPanel _logPanel;

    private void OnEsc()
    {
        if (_settingPanel.IsActive)
            _settingPanel.DisableUI();
        if(_logPanel.IsActive)
            _logPanel.DisableUI();
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
