using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AgentMovement
{
    public Action OnMovementEvent;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PlatformInfo _currentPlatformInfo;
    private Vector3 newPlatformHit;
    private bool _isGround;
    private Vector3 _groundDetectSize = Vector3.one * 0.5f;

    [SerializeField]
    private float _limitedAirHoldTime = 10f;

    private float _airHoldTime = 0;

    protected override void Update()
    {
        CheckGround();
        if (_isStun)
        {
            return;
        }
        
        if (_isMoving)
        {

            _currentMoveTime += Time.deltaTime;
            float ratio = _currentMoveTime / _moveTime;
            //transform.position = Vector3.Lerp(_beforePosition, _targetPos, ratio);
            _visualTrm.rotation = Quaternion.Slerp(Quaternion.identity, _targetRotate, EasingFunction.EaseInCircle(ratio));
            if (ratio >= 1f)
            {
                print("움직임 끝남");
                _currentMoveTime = 0;
                _visualTrm.rotation = Quaternion.identity;
                _isMoving = false;
            }
        }
    }

    public override bool Move(Vector3 direction)
    {
        if (_isMoving || _isStun || !_isGround) return false;
        
        int x = Mathf.Clamp((int)(direction.x), -1, 1);
        int z = Mathf.Clamp((int)(direction.z), -1, 1);
        Vector3 totalDirection = new Vector3(
            x,
            0,
            x == 0 ? z : 0
        ).normalized * _moveCell;
        MoveDirection = totalDirection;
        if (totalDirection.magnitude < 0.1f || !DetectObstacle()) return false;
        if (!DetectInteraction()) return false;

        _isMoving = true;

        _targetPos = transform.position + totalDirection;
        _targetRotate = Quaternion.Euler(totalDirection.z * 180f, 0, totalDirection.x * -180f);
        transform.DOJump(_targetPos, _jumpScale, 1, _moveTime);
        OnMovementEvent?.Invoke();
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

    private void CheckGround()
    {
        Physics.BoxCast(transform.position, _groundDetectSize, Vector3.down, out RaycastHit hit, Quaternion.identity, 2f, _groundLayer);
        if (hit.collider != null)
        {
            _isGround = true;
            _airHoldTime = 0;
            return;
        }
        
        _airHoldTime += Time.deltaTime;
        _isGround = false;
        if (_airHoldTime >= _limitedAirHoldTime)
        {
            _agent.HealthCompo.TakeDamage(99);
        }
    }
    
}
