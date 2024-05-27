using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Agent
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlatformInfo _currentPlatformInfo;

    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //OnLeftClick();
    }

   

    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        Move(dir);
    }


    public bool Move(PlayerInputDirection inputDir)
    {
        Vector3 direction = VectorCalculate.GetMoveDirection(inputDir, _currentPlatformInfo.localDirection);
        if (_isMoving) return false;
        switch (_currentPlatformInfo.localDirection)
        {
            case LocalDirection.Default:
                int x = Mathf.Clamp(Mathf.FloorToInt(direction.x), -1, 1);
                int z = Mathf.Clamp(Mathf.FloorToInt(direction.z), -1, 1); 
                MoveDirection = new Vector3(x, 0,  x == 0 ? z : 0).normalized * _moveCell;
                
                // x = Mathf.Clamp(Mathf.FloorToInt(direction.normalized.x), -1, 1);
                // z = Mathf.Clamp(Mathf.FloorToInt(direction.normalized.z), -1, 1);
                // MoveDirection = new Vector3(x, 0,  x == 0 ? z : 0).normalized * _moveCell;
                break;
    
            case LocalDirection.Left:
                int xForRight = Mathf.Clamp(Mathf.FloorToInt(direction.x), -1, 1);
                int yForRight = Mathf.Clamp(Mathf.FloorToInt(direction.y), -1, 1);
                MoveDirection = new Vector3(xForRight,  xForRight == 0 ? yForRight : 0, 0).normalized * _moveCell;
                
                // xForRight = Mathf.Clamp(Mathf.FloorToInt(direction.normalized.x), -1, 1);
                // yForRight = Mathf.Clamp(Mathf.FloorToInt(direction.normalized.y), -1, 1);
                // MoveDirection = new Vector3(xForRight,  xForRight == 0 ? yForRight : 0, 0).normalized * _moveCell;

                break;
    
            case LocalDirection.Right:
                int yForLeft = Mathf.Clamp(Mathf.FloorToInt(direction.y), -1, 1);
                int zForLeft = Mathf.Clamp(Mathf.FloorToInt(direction.z), -1, 1);
                MoveDirection = new Vector3(0, yForLeft, yForLeft == 0 ? zForLeft : 0).normalized * _moveCell;
                break;
        }
    
        if (MoveDirection.magnitude < 0.1f || !DetectObstacle()) return false;

        bool isHitNewPlatform = Physics.BoxCast(transform.position, Vector3.one, MoveDirection, out RaycastHit hit, Quaternion.identity, 7f, _groundLayer);
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
        DetectInteraction();
    
        _targetPos = transform.position + MoveDirection;
        _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
    
        _isMoving = true;
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime).OnComplete(() => _isMoving = false);
        transform.DOMove(_targetPos, _moveTime).OnComplete(()=> _isMoving = false);
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
