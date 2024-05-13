using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingCameraObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private Transform _currentFollowingTarget;

    private void Start()
    {
        SetTarget(_playerTrm);
    }
    

    private void Update()
    {
        transform.position = _currentFollowingTarget.position;
    }

    public void SetTarget(Transform target)
    {
        _currentFollowingTarget = target;
    }
}
