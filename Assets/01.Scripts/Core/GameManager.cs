using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    //public StageManager stageManager { get; private set; }
    //public LevelManager levelManager { get; private set; }
    
    private Transform _managersTrm;
    
    private void Awake()
    {
        _managersTrm = transform.parent;
        //stageManager = _managersTrm.GetComponentInChildren<StageManager>();
        //levelManager = _managersTrm.GetComponentInChildren<LevelManager>();
        
        
        
    }

    public void ExitInGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
}