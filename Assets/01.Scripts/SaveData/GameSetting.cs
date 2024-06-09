using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting
{
    public int bgmVolume;
    public int sfxVolume;

    private void LoadSetting()
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
