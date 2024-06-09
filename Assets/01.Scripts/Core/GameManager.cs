using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public StageManager stageManager { get; private set; }
    public LevelManager levelManager { get; private set; }
    
    private Transform _managersTrm;
    
    private void Awake()
    {
        _managersTrm = transform.parent;
        stageManager = _managersTrm.GetComponent<StageManager>();
        levelManager = _managersTrm.GetComponent<LevelManager>();
        
        
        
    }
    
}