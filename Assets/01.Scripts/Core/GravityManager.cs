using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GravityManager : MonoSingleton<GravityManager>
{
    public void SetGravity(LocalDirection gravityDirection = LocalDirection.Default)
    {

        Debug.Log("중력변경됨");
        switch (gravityDirection)
        {
            case LocalDirection.Default:
                CameraManager.Instance.RotateReset();
                Physics.gravity = Vector3.down * 9.8f;
                break;
            case LocalDirection.Left:
                CameraManager.Instance.RotateLeft();
                Physics.gravity = Vector3.forward * 9.8f;
                break;
            case LocalDirection.Right:
                CameraManager.Instance.RotateRight();
                Physics.gravity = Vector3.right * 9.8f;
                break;
            default:
                Debug.LogWarning("can't Apply Gravity");
                break;
        }
    }
    

}