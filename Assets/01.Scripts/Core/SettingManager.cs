using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private GameSetting _gameSetting;

    [SerializeField] private SettingPanel _settingPanel;

    private void Start()
    {
        _gameSetting = new GameSetting();
        Load();
        _settingPanel._BGMSlider.value = _gameSetting.bgmVolume;
        _settingPanel._SFXSlider.value = _gameSetting.sfxVolume;
        
        _settingPanel._BGMSlider.onValueChanged.AddListener(HandleBGMSliderValueChanged);
        _settingPanel._SFXSlider.onValueChanged.AddListener(HandeSFXSliderValueChanged);
        _settingPanel._timeSceleSlider.onValueChanged.AddListener(HandleTimeScaleValueChanged);
    }

    public void Load()
    {
        _gameSetting.LoadSetting();

    }

    public void Save()
    {
        _gameSetting.SaveSetting();
    }
    
    private void HandleBGMSliderValueChanged(float value)
    {
        _gameSetting.bgmVolume = value;
    }
    
    private void HandeSFXSliderValueChanged(float value)
    {
        _gameSetting.sfxVolume = value;
    }
    
    private void HandleTimeScaleValueChanged(float value)
    {
        // 제어장치 추가필요
        Time.timeScale = value;
    }
}