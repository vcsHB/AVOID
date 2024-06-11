using System;
using System.Collections;
using UnityEngine;

public class PlatformGroup : MonoBehaviour
{
    [SerializeField] private Transform _playerStartPositionTrm;

    [SerializeField] private PlatformObject[] platforms;
    public Vector3 playerStartPos => _playerStartPositionTrm.position;

    private void Awake()
    {
        platforms = GetComponentsInChildren<PlatformObject>();
    }

    public void GeneratePlatforms()
    {
        
        StartCoroutine(GenerateCoroutine());
    }

    private IEnumerator GenerateCoroutine()
    {  
        WaitForSeconds ws = new WaitForSeconds(0.2f);
        for (int i = 0; i < platforms.Length; i++)
        {
            yield return ws;
            platforms[i].Generate();
        }
    }
    
    [ContextMenu("DebugAllDestroy")]
    public void DestroyPlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].Destroy();
        }
    }

    public void Rotate()
    {
        //
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        yield return null;
    }

}