using System.Collections;
using StageManage;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private StageSO _currentStage;
    [SerializeField] private Transform _stageBaseTrm;
    [SerializeField] private Level _currentStageLevel;
    public StageSO CurrentStage => _currentStage;
    [SerializeField] private bool _startLevelLoad = true;
    
    private void Start()
    {
        if(_startLevelLoad)
            ResetLevel();
    }

    public void SetStage(StageSO stage)
    {
        _currentStage = stage;
        if(_startLevelLoad)
            ResetLevel();
    }

    public Coroutine ResetLevel()
    {
         return StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        if (_currentStageLevel != null) 
            _currentStageLevel.Destroy();
        

        yield return new WaitForSeconds(0.5f);
        Destroy(_currentStageLevel.gameObject);
        _currentStageLevel = Instantiate(_currentStage.levelPrefab, _stageBaseTrm);
        PlayerManager.Instance.PlayerTrm.position = _currentStageLevel.playerStartPos;

    }
}
