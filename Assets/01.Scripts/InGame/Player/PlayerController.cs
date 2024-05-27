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


    public override bool Move(Vector3 direction)
    {
        if (_isMoving) return false;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        MoveDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
        ).normalized * _moveCell;
        if (MoveDirection.magnitude < 0.1f) return false;
        DetectInteraction();

        _targetPos = transform.position + MoveDirection;
        _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        _isMoving = true;
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
