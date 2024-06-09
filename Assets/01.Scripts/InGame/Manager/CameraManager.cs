using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _virCam;
    [SerializeField] private PlayerFollowingCameraObject _followingObject;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    [Header("Setting Values")] 
    [SerializeField] private float _rotateDuration = 1f;

    private bool _isShaking;

    private void Awake()
    {
        _cinemachineBasicMultiChannelPerlin =
            _virCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }

    public void SetFollow(Transform followTarget)
    {
        _followingObject.SetTarget(followTarget);
    }
    

    public void SetFollowOff()
    {
        _followingObject.SetFollowingState(false);
    }

    public void SetFollowOn()
    {
        _followingObject.SetFollowingState(true);
    }
        
    [ContextMenu("DebugReset")]
    public void RotateReset()
    {
        RotateCamera(0, _rotateDuration);
    }
    [ContextMenu("DebugLeft")]
    public void RotateLeft()
    {
        RotateCamera(-125, _rotateDuration);
    }
    [ContextMenu("DebugRight")]
    public void RotateRight()
    {
        RotateCamera(125, _rotateDuration);
    }

    public void RotateCamera(int rotate, float duration = 1)
    {
        StopAllCoroutines();
        StartCoroutine(RotateCameraCoroutine(rotate, duration));
    }

    private IEnumerator RotateCameraCoroutine(int rotate, float duration)
    {
        float currentTime = 0;
        float beforeValue = _virCam.m_Lens.Dutch;
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            _virCam.m_Lens.Dutch =
                    Mathf.Lerp(beforeValue, rotate, EasingFunction.EaseInOutQuint(currentTime / duration));
            
            yield return null;
        }

        _virCam.m_Lens.Dutch = rotate;
    }

    public void Shake(float shakePower, float duration)
    {
        if (_isShaking) return;
        StartCoroutine(ShakeCoroutine(shakePower, duration));
    }

    private IEnumerator ShakeCoroutine(float power, float duration)
    {
        _isShaking = true;
        SetShake(power, power / 2);
        yield return new WaitForSeconds(duration);
        ShakeOff();
        _isShaking = false;
    }
    

    public void SetShake(float Amplitude, float frequency)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Amplitude;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

    }

    public void ShakeOff()
    {
        SetShake(0,0);
    }
}
