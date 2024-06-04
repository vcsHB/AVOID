using System;
using System.Collections;
using UnityEngine;

public class PlatformGenerator : LogicObject
{
    [SerializeField] private PlatformObject[] _targetPlatforms;
    [SerializeField] private bool _useTerm;
    [SerializeField] private float _platformGenerateTerm;


    
    private void Start()
    {
        logicSolvedEvent.AddListener(HandleGeneratePlatform);
    }

    public void HandleGeneratePlatform()
    {
        if (_useTerm)
        {
            GeneratePlatformsWithTerm();
        }
        else
        {
            GeneratePlatform();
        }
    }

    public void GeneratePlatform()
    {
        for (int i = 0; i < _targetPlatforms.Length; i++)
        {
            _targetPlatforms[i].Generate();
        }
    }

    private void GeneratePlatformsWithTerm()
    {
        StartCoroutine(GenerateWithTermCoroutine());
    }

    private IEnumerator GenerateWithTermCoroutine()
    {
        WaitForSeconds ws = new WaitForSeconds(_platformGenerateTerm);
        for (int i = 0; i < _targetPlatforms.Length; i++)
        {
            _targetPlatforms[i].Generate();
            yield return ws;
        }
    }
}