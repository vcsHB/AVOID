using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private StageSO _currentStage;
    [SerializeField] private Transform _stageBaseTrm;
    [SerializeField] private Level _currentStageLevel;
    
    
    public void ResetLevel()
    {
        
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }
}
