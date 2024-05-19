using System;
using System.Collections;
using UnityEngine;

public enum LocalDirection
{
    Default = 0,
    Left = -125,
    Right = 125
}

public class PlatformObject : MonoBehaviour
{
    [SerializeField] private PlatformInfo _platformInfo;
    public PlatformInfo PlatformInfo => _platformInfo;
    [SerializeField] private float _generateDuration = 1.5f;
    [SerializeField] private float _destoryDuration = 1.5f;
    [SerializeField] private Transform _platformTrm;
    private Collider _collider;

    private void Awake()
    {
        _collider = _platformTrm.GetComponent<Collider>();
    }

    [ContextMenu("Init")]
    public void Generate()
    {

        StartCoroutine(AppearCoroutine());
    }

    private IEnumerator AppearCoroutine()
    {
        float currentTime = 0;
        Vector3 targetPos = transform.position;
        Vector3 beforePos = new Vector3(targetPos.x, targetPos.y +10, targetPos.z);
        while (currentTime <= _generateDuration)
        {
            if(TimeManager.TimeScale == 0) continue;
            float ratio = currentTime / _generateDuration;
            _platformTrm.localPosition = Vector3.Lerp(beforePos, targetPos, EasingFunction.EaseInOutCubic(ratio));
            currentTime += TimeManager.TimeScale * Time.deltaTime;
            yield return null;
        }
        _platformTrm.localPosition = targetPos;
        
    }

    public void Destroy()
    {
        StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator DisappearCoroutine()
    {
        float currentTime = 0;
        Vector3 beforePos = transform.position;
        Vector3 targetPos = new Vector3(beforePos.x, beforePos.y-10, beforePos.z);
        while (currentTime <= _destoryDuration)
        {
            if(TimeManager.TimeScale == 0) continue;
            float ratio = currentTime / _destoryDuration;
            _platformTrm.localPosition = Vector3.Lerp(beforePos, targetPos, EasingFunction.EaseInOutCubic(ratio));
            currentTime += TimeManager.TimeScale * Time.deltaTime;
            yield return null;
        }
        _platformTrm.localPosition = targetPos;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    
    
    
}