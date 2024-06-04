using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private PlatformGroup _platformGroup;

    public Vector3 playerStartPos => _platformGroup.playerStartPos;

    public void Destroy()
    {
        _platformGroup.DestroyPlatforms();
    }
    
}
