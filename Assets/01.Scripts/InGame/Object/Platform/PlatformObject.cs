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
    [SerializeField] private float _destroyTerm = 2f;
    [SerializeField] private float _destoryDuration = 1.5f;
    [SerializeField] private Transform _platformTrm;
    [SerializeField] private bool _isActive;
    private Collider _collider;
    private MeshRenderer _platformRenderer;
    private MeshRenderer _DeadZoneRenderer;
    private Material _deadZoneMaterial;
    private int _deadZoneAlphaHash;
    private Transform _objectsTrm;

    private void Awake()
    {
        _collider = _platformTrm.GetComponent<Collider>();
        _DeadZoneRenderer = _platformTrm.Find("DeadZone").GetComponent<MeshRenderer>();
        _platformRenderer = _platformTrm.GetComponent<MeshRenderer>();
        _deadZoneMaterial = _DeadZoneRenderer.material;
        _deadZoneAlphaHash = Shader.PropertyToID("_Alpha");
        _objectsTrm = _platformTrm.Find("Objects");
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (_isActive) return;
        _isActive = true;
        StartCoroutine(AppearCoroutine());
    }
    
    [ContextMenu("Destroy")]
    public void Destroy()
    {
        if (!_isActive) return;
        _isActive = false;
        StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator AppearCoroutine()
    {
        _collider.enabled = true;
        _platformRenderer.enabled = true;
        _DeadZoneRenderer.enabled = true;
        float currentTime = 0;
        Vector3 targetPos = transform.position;
        Vector3 beforePos = targetPos + _platformInfo.NormalDirection * 10;
        while (currentTime <= _generateDuration)
        {
            //if(TimeManager.TimeScale == 0) continue;
            float ratio = currentTime / _generateDuration;
            _platformTrm.position = Vector3.Lerp(beforePos, targetPos, EasingFunction.EaseInOutCubic(ratio));
            currentTime += Time.deltaTime;
            yield return null;
        }
        _platformTrm.position = targetPos;
        _DeadZoneRenderer.enabled = false;
    }
    
    

    private IEnumerator DisappearCoroutine()
    {
        float currentTime = 0;
        _DeadZoneRenderer.enabled = true;
        StartCoroutine(SetDeadZone(true, 0.5f));
        yield return new WaitForSeconds(_destroyTerm);
        _collider.enabled = false;
        _DeadZoneRenderer.enabled = false;

        Vector3 beforePos = transform.position;
        Vector3 targetPos = beforePos + (-_platformInfo.NormalDirection * 10);
        while (currentTime <= _destoryDuration)
        {
            //if(TimeManager.TimeScale == 0) continue;
            float ratio = currentTime / _destoryDuration;
            _platformTrm.position = Vector3.Lerp(beforePos, targetPos, EasingFunction.EaseInOutCubic(ratio));
            currentTime +=  Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SetDeadZone(false, 0.5f));

        _platformTrm.position = targetPos;
        yield return new WaitForSeconds(0.2f);
        _platformRenderer.enabled = false;
        //Destroy(gameObject);
    }

    private IEnumerator SetDeadZone(bool value, float settingDuration)
    {
        int beforeValue, targetValue;
        if (value)
        {
            beforeValue = 0;
            targetValue = 1;
        }
        else
        {
            beforeValue = 1;
            targetValue = 0;
        }

        float currentTime = 0;
        while (currentTime <= settingDuration)
        {
            currentTime += Time.deltaTime;
            float ratio = Mathf.Lerp(beforeValue, targetValue, currentTime / settingDuration);
            _deadZoneMaterial.SetFloat(_deadZoneAlphaHash, ratio);
            yield return null;
        }
    }
    
    
    
}