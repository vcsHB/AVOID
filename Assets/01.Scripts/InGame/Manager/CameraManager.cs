using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virCam;
    [SerializeField] private PlayerFollowingCameraObject _followingObject;

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
        RotateCamera(0, 2);
    }
    [ContextMenu("DebugLeft")]
    public void RotateLeft()
    {
        RotateCamera(-125, 2);
    }
    [ContextMenu("DebugRight")]
    public void RotateRight()
    {
        RotateCamera(125, 2);
    }

    public void RotateCamera(int rotate, float duration = 1)
    {
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
}
