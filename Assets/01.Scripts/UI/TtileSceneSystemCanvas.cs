using UnityEngine;

public class TtileSceneSystemCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private TitleSceneSettingPanel _settingPanel;
    [SerializeField] private LogPanel _logPanel;
    [SerializeField] private TutorialPanel _tutorialPanel;
    [SerializeField] private NormalPanel[] _normalPanels;
    private void OnEsc()
    {
        if (_settingPanel.IsActive)
            _settingPanel.DisableUI();
        for (int i = 0; i < _normalPanels.Length; i++)
        {
            if(_normalPanels[i].IsActive)
                _normalPanels[i].DisableUI();
        }
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
