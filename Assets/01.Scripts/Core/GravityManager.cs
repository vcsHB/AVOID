using UnityEngine;

public class GravityManager : MonoSingleton<GravityManager>
{
    public void SetGravity(PlatformInfo platformInfo)
    {

        Debug.Log("중력변경됨");
        switch (platformInfo.localDirection)
        {
            case LocalDirection.Default:
                CameraManager.Instance.RotateReset();
                break;
            case LocalDirection.Left:
                CameraManager.Instance.RotateLeft();
                break;
            case LocalDirection.Right:
                CameraManager.Instance.RotateRight();
                break;
            default:
                Debug.LogWarning("can't Apply Gravity");
                break;
        }
        PlayerVirtualRigidbody.PlayerGravity = -platformInfo.NormalDirection * 9.8f;

    }

    
    

}