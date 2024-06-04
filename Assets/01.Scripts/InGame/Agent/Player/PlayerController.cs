using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AgentMovement
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlatformInfo _currentPlatformInfo;
    private Vector3 newPlatformHit;
   
    
    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        Move(dir);
    }

    public override bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        Vector3 totalDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
        ).normalized * _moveCell;
        MoveDirection = totalDirection;
        if (totalDirection.magnitude < 0.1f || !DetectObstacle()) return false;
        DetectInteraction();

        _isMoving = true;

        _targetPos = transform.position + totalDirection;
        _targetRotate = Quaternion.Euler(totalDirection.z * 180f, 0, totalDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime).OnComplete(() => _isMoving = false);
        // 새 플랫폼 감지를 추가해야함
        if (CheckNewPlatform())
        {
            print("New Platform");
        }
        
        return true;
    }

    private bool CheckNewPlatform()
    {
        RaycastHit[] hitGrounds = new RaycastHit[4];

        int hitGroundAmount = Physics.BoxCastNonAlloc(transform.position, Vector3.one, MoveDirection.normalized, hitGrounds, Quaternion.identity, 7f, _groundLayer);

        if (hitGroundAmount == 0) return false;

        for (int i = 0; i < hitGroundAmount; i++)
        {
            if (hitGrounds[i].transform.parent.TryGetComponent(out PlatformObject platform))
            {
                if (platform.PlatformInfo.localDirection == _currentPlatformInfo.localDirection)
                {
                    continue;
                }

                newPlatformHit = hitGrounds[i].point;
                _currentPlatformInfo = platform.PlatformInfo;
                return true;
            }
        }

        return false;
    }

    private void ChangeGravity()
    {
        
        // 부모를 변경하는 방식으로 플랫폼에 붙을 것인가?
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection);
        Gizmos.DrawWireCube(newPlatformHit, Vector3.one * 2);
    }
}
