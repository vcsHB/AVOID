using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingCameraObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private Transform _currentFollowingTarget;
    private bool isFollowing = true;
    
    private void Start()
    {
        SetTarget(_playerTrm);
    }
    

    private void Update()
    {
        if (!isFollowing) return;
            
        transform.position = _currentFollowingTarget.position;
    }

    public void SetTarget(Transform target)
    {
        _currentFollowingTarget = target;
    }

    /**
     * <summary>
     * 플레이어로 타겟을 설정함
     * </summary>
     */
    public void SetTargetReset()
    {
        SetTarget(_playerTrm);
    }

    public void SetFollowingState(bool value)
    {
        isFollowing = value;
    }
}

