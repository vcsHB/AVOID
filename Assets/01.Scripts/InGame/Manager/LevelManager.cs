using System.Collections;
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

    public Coroutine ResetLevel()
    {
         return StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        if (_currentStageLevel != null) 
            _currentStageLevel.Destroy();
        
        PlayerManager.Instance.PlayerTrm.position = _currentStageLevel.playerStartPos;

        yield return new WaitForSeconds(0.5f);
        Destroy(_currentStageLevel.gameObject);
        _currentStageLevel = Instantiate(_currentStage.levelPrefab, _stageBaseTrm);
    }
}
