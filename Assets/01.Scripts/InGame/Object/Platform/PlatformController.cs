using System;
using System.Collections;
using System.Collections.Generic;
using Platform;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformGroup _platformGroup;
    [HideInInspector] public PlatformObject[] _platforms;
    [SerializeField] private PlatformAnimationSequence[] _sequences;
    [SerializeField] private bool _isLoop;

    private float _currentTime = 0;
    [SerializeField]
    private int _currentSequenceIndex;

    private void Awake()
    {
        for (int i = 0; i < _sequences.Length; i++)
        {
            _sequences[i].Initialize(this);
        }
    }

    private void Start()
    {
        _platforms = _platformGroup.platforms;
        StartSequence();
    }

    private void Update()
    {
        
        


    }

    private void StartSequence()
    {
        StartCoroutine(StartSequenceCoroutine());
    }

    private IEnumerator StartSequenceCoroutine()
    {
        do
        {
            yield return _sequences[_currentSequenceIndex].PlaySequence();
            _currentSequenceIndex = (_currentSequenceIndex + 1) % _sequences.Length;

        } while (_isLoop);
    }
    
    
}
