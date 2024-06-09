public class GameSetting
{
    public float bgmVolume;
    public float sfxVolume;

    public void LoadSetting()
    {
        GameSetting setting = DBManager.GetGameSetting();
        bgmVolume = setting.bgmVolume;
        sfxVolume = setting.sfxVolume;
    }

    public void SaveSetting()
    {
        DBManager.SaveGameSetting(this);
    }
}
