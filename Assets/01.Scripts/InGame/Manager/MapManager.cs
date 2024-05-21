using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private PlatformObject _platformPrefab;

    public List<PlatformObject> platformList = new List<PlatformObject>();

    
    public void DestroyAllPlatformImmediate()
    {
        foreach (PlatformObject platform in platformList)
        {
            platform.Destroy();
        }
    }
    public void DestroyAllPlatform()
    {
        StartCoroutine(DestroyAllCoroutine());
    }

    private IEnumerator DestroyAllCoroutine()
    {
        WaitForSeconds ws = new WaitForSeconds(0.1f);
        foreach (PlatformObject platform in platformList)
        {
            platform.Destroy();
            yield return ws;
        }
    }
    
    private void GeneratePlatform(Vector3 position)
    {
        // 풀링으로 갈아 끼워야 함
        PlatformObject platform = Instantiate(_platformPrefab, position, Quaternion.identity);
        platform.Generate();
    }
}
