using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private StageSO _currentStage;
    [SerializeField] private Transform _stageBaseTrm;
    [SerializeField] private Level _currentStageLevel;

    private void Start()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        if (_currentStageLevel != null) 
            _currentStageLevel.Destroy();
        
        yield return new WaitForSeconds(2f);
        Destroy(_currentStageLevel.gameObject);
        _currentStageLevel = Instantiate(_currentStage.levelPrefab, _stageBaseTrm);
        PlayerManager.Instance.PlayerTrm.position = _currentStageLevel.playerStartPos;
    }
}
