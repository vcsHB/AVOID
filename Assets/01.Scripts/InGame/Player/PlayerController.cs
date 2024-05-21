using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Agent
{
    private Vector3 _mousePos;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlatformInfo _currentPlatformInfo;

    private void FixedUpdate()
    {
        //OnLeftClick();
    }

    public void OnLeftClick()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.collider == null) return;
        _mousePos = hit.point;
        Move(_mousePos - transform.position);
    }

    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        Move(dir);
    }

    // protected override bool Move(Vector3 direction)
    // {
    //     if (_isMoving) return false;
    //     
    //     int x = Mathf.Clamp((int)(direction.x), -1, 1);
    //     int y = Mathf.Clamp((int)(direction.y), -1, 1);
    //     int z = Mathf.Clamp((int)(direction.z), -1, 1);
    //     MoveDirection = new Vector3(
    //         x,
    //         0,
    //         x == 0 ? z : 0
    //     ).normalized * _moveCell;
    //     if (MoveDirection.magnitude < 0.1f) return false;
    //     DetectInteraction();
    //     
    //     _targetPos = transform.position + MoveDirection;
    //
    //     bool isHitNewPlatform = Physics.Raycast(transform.position + Vector3.up, MoveDirection, out RaycastHit hit, 3f, _groundLayer);
    //     if (isHitNewPlatform)
    //     {
    //         print("새 플랫폼 감지됨");
    //         if (hit.transform.parent.TryGetComponent(out PlatformObject platform))
    //         {
    //             if (_gravityDirection != platform.PlatformDirection)
    //             {
    //                 _gravityDirection = platform.PlatformDirection;
    //                 GravityManager.Instance.SetGravity(_gravityDirection);
    //                 
    //             }
    //         }
    //     }
    //
    //     switch (_gravityDirection)
    //     {
    //         case LocalDirection.Default:
    //             _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
    //             break;
    //         case LocalDirection.Left:
    //             _targetRotate = Quaternion.Euler(MoveDirection.y * 180f, MoveDirection.x * 180, 0);
    //             break;
    //         case LocalDirection.Right:
    //             _targetRotate = Quaternion.Euler(0, MoveDirection.z * 180, MoveDirection.y * 180f);
    //             break;
    //         
    //     }
    //     //_targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
    //     transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
    //     _isMoving = true;
    //     return true;
    // }

    protected override bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        switch (_currentPlatformInfo.localDirection)
        {
            case LocalDirection.Default:
                print("Default 방향");
                int x = Mathf.Clamp((int)direction.x, -1, 1);
                int z = Mathf.Clamp((int)direction.z, -1, 1);
                MoveDirection = new Vector3(x, 0,  x == 0 ? z : 0).normalized * _moveCell;
                break;
    
            case LocalDirection.Left:
                int xForRight = Mathf.Clamp((int)direction.x, -1, 1);
                int yForRight = Mathf.Clamp((int)direction.y, -1, 1);
                MoveDirection = new Vector3(xForRight,  xForRight == 0 ? yForRight : 0, 0).normalized * _moveCell;
                
                break;
    
            case LocalDirection.Right:
                int xForLeft = Mathf.Clamp((int)direction.x, -1, 1);
                int yForLeft = Mathf.Clamp((int)direction.y, -1, 1);
                MoveDirection = new Vector3(xForLeft, xForLeft == 0 ? yForLeft : 0, 0).normalized * _moveCell;
                break;
        }
    
        if (MoveDirection.magnitude < 0.1f) return false;
    
        bool isHitNewPlatform = Physics.Raycast(transform.position, MoveDirection.normalized, out RaycastHit hit, 7f, _groundLayer);
        if (isHitNewPlatform)
        {
            print("새 플랫폼 감지됨");
            if (hit.transform.parent.TryGetComponent(out PlatformObject platform))
            {
                if (_currentPlatformInfo.localDirection != platform.PlatformInfo.localDirection)
                {
                    _targetPos = (transform.position + MoveDirection.normalized * 4 )+ _currentPlatformInfo.NormalDirection * 4;
                    //_targetPos = VectorCalculate.CalcSafeVector(transform.position, _currentPlatformInfo.NormalDirection * 1.95f);
                    _isMoving = true;
                    print(_targetPos);
                    _rigid.useGravity = false;
                    transform.DOMove(_targetPos, _moveTime).OnComplete(() =>
                    {
                        _currentPlatformInfo = platform.PlatformInfo;
                        GravityManager.Instance.SetGravity(platform.PlatformInfo);
                        _rigid.useGravity = true;
                        transform.position = _targetPos;
                        _isMoving = false;
                    });
                    return true;
                }
            }
        }
    
        // Detect interaction if any
        DetectInteraction();
    
        // Set the target position and rotation
        _targetPos = transform.position + MoveDirection;
        _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
    
        // Use DOJump to move and set _isMoving to true
        _isMoving = true;
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime).OnComplete(() => _isMoving = false);
    
        return true;
    }


    private void ChangeGravity()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection);
    }
}
