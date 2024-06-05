using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : WindowPanel
{
    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _SFxSlider;
    [SerializeField] private Slider _timeSceleSlider;
    public override void ShowUI()
    {
    }

    public override void DisableUI()
    {
        
    }

    public void Exit()
    {
        
    }
}