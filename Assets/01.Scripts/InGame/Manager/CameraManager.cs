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
}
