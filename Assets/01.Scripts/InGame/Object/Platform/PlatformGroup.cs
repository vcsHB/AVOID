using System.Collections;
using UnityEngine;

public class PlatformGroup : MonoBehaviour
{
    [SerializeField] private LogicObject mainLogic;
    [SerializeField] private PlatformObject[] platforms;

    
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