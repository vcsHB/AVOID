using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private Player _player;
    private PlayerController _playerController;
    
    private Vector3 _mousePos;
    private Vector3 _screenCenter;
    [SerializeField] private bool _isMouseUp;
    [SerializeField] private LayerMask _detectZoneLayer;

    private Vector3 _rayDirection;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
    }
    
    #region Main Player Input

    public void OnLeftClick()
    {
        print("좌클릭");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 300f, _detectZoneLayer);
        if (hit.collider == null) return;
        print("바닥 맞음");
        _rayDirection = hit.point - transform.position;
        _playerController.Move(_rayDirection);
    }
    
    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        bool result =_playerController.Move(dir);
    }

    public void OnRetry()
    {
        _player.HandleAgentDie();
    }
    
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _rayDirection);
    }
}
