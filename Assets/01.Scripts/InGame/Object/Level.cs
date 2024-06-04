using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _playerStartPositionTrm;
    [SerializeField]
    private PlatformGroup _platformGroup;

    public Vector3 playerStartPos => _playerStartPositionTrm.position;
    
    
    
}
