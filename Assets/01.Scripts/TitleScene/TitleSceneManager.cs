using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoSingleton<TitleSceneManager>
{
    [SerializeField] private TitleSceneSettingPanel _settingPanel;
    public bool canControl = true;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneNames.InGame);
    }

    public void StartStageSelect()
    {
        
        SceneManager.LoadScene(SceneNames.InGame);
        LevelManager.Instance.SetStage(StageManager.Instance.stageList.FindStage(0));

    }   
    
    
    

    public void ShowSettingPanel()
    {
        _settingPanel.ShowUI();
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    
}
