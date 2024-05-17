using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = System.Diagnostics.Debug;

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

    protected override bool Move(Vector3 direction)
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

        bool isHitNewPlatform = Physics.Raycast(transform.position + Vector3.up, MoveDirection, out RaycastHit hit, 3f, _groundLayer);
        if (isHitNewPlatform)
        {
            print("새 플랫폼 감지됨");
            if (hit.transform.TryGetComponent(out PlatformObject platform))
            {
                if (_gravityDirection != platform.PlatformDirection)
                {
                    _gravityDirection = platform.PlatformDirection;
                    GravityManager.SetGravity(_gravityDirection);
                    //Move();
                }
            }
        }
        
        _targetRotate = Quaternion.Euler(MoveDirection.z * 180f, 0, MoveDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        _isMoving = true;
        return true;
    }

    private void ChangeGravity()
    {
        
    }


    
}
