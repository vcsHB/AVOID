using System.IO;
using StageManage;
using UnityEngine;

public class DBManager
{
    private static string LOCALPATH = Application.dataPath+"/SaveData";
    private static string StageSaveFileName = "StageData.json";
    private static string GameSettingFileName = "GameSetting.json";

    
    public static GameSetting GetGameSetting()
    {
        string path = Path.Combine(LOCALPATH, GameSettingFileName);
        CheckLocalPath();
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            return JsonUtility.FromJson<GameSetting>(data);
        }

        GameSetting newSetting = new GameSetting();
        SaveGameSetting(newSetting);
        return newSetting;
    }

    public static void SaveGameSetting(GameSetting gameSetting)
    {
        CheckLocalPath();
        string json = JsonUtility.ToJson(gameSetting);
        string path = Path.Combine(LOCALPATH, GameSettingFileName);
        File.WriteAllText(path, json);

    }

    public static StageDataList GetStageData()
    {
        CheckLocalPath();
        string path = Path.Combine(LOCALPATH, StageSaveFileName);
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            return JsonUtility.FromJson<StageDataList>(data);
        }

        StageDataList newData = new StageDataList();
        SaveStageData(newData);
        return newData;
    }

    public static void SaveStageData(StageDataList stage)
    {
        CheckLocalPath();
        string json = JsonUtility.ToJson(stage);
        string path = Path.Combine(LOCALPATH, StageSaveFileName);
        File.WriteAllText(path, json);

    }

    private static void CheckLocalPath()
    {
        if (!Directory.Exists(LOCALPATH))
        {
            Debug.Log("폴더가 존재하지 않습니다.");
            Debug.Log("폴더를 생성합니다.");
            Directory.CreateDirectory(LOCALPATH);
        }
    }
    
    
}