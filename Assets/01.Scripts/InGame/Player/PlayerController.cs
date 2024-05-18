using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Agent
{
    private Vector3 mousePos;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LocalDirection _gravityDirection;

    private void FixedUpdate()
    {
        //OnLeftClick();
    }

    public void OnLeftClick()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 20);
        if (hit.collider == null) return;
        mousePos = hit.point;
        Move(mousePos - transform.position);
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
    //                 GravityManager.SetGravity(_gravityDirection);
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
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    //     //_targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
    //     transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
    //     _isMoving = true;
    //     return true;
    // }

    protected override bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        switch (_gravityDirection)
        {
            case LocalDirection.Default:
                print("Default 방향");
                int x = Mathf.Clamp((int)direction.x, -1, 1);
                int z = Mathf.Clamp((int)direction.z, -1, 1);
                MoveDirection = new Vector3(x, 0, z).normalized * _moveCell;
                break;

            case LocalDirection.Left:
                int xForRight = Mathf.Clamp((int)direction.x, -1, 1);
                int yForRight = Mathf.Clamp((int)direction.y, -1, 1);
                MoveDirection = new Vector3(xForRight, yForRight, 0).normalized * _moveCell;
                
                break;

            case LocalDirection.Right:
                int xForLeft = Mathf.Clamp((int)direction.x, -1, 1);
                int yForLeft = Mathf.Clamp((int)direction.y, -1, 1);
                MoveDirection = new Vector3(xForLeft, yForLeft, 0).normalized * _moveCell;
                break;
        }

        if (MoveDirection.magnitude < 0.1f) return false;

        bool isHitNewPlatform = Physics.Raycast(transform.position, MoveDirection, out RaycastHit hit, 3f,
            _groundLayer);
        if (isHitNewPlatform)
        {
            print("새 플랫폼 감지됨");
            if (hit.transform.parent.TryGetComponent(out PlatformObject platform))
            {
                if (_gravityDirection != platform.PlatformDirection)
                {
                    // Move towards the platform
                    _targetPos = transform.position + MoveDirection;
                    _isMoving = true;
                    transform.DOJump(_targetPos, _jumpScale, 1, _moveTime).OnComplete(() =>
                    {
                        // Attach to the new platform after moving
                        _gravityDirection = platform.PlatformDirection;
                        GravityManager.Instance.SetGravity(_gravityDirection);
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



}
