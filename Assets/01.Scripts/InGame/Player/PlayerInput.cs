using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    
    private Vector3 _mousePos;
    private Vector3 _screenCenter;
    [SerializeField] private bool _isMouseUp;


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
        //if()
        //print(_mousePos);
    }
    
    #region Main Player Input

    
    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        _playerController.Move(dir);
    }
    
    // public void OnKeyMoveW()
    // {
    //     print("W input");
    //     _playerController.Move(PlayerInputDirection.LeftUp);
    // }
    //
    // public void OnKeyMoveA()
    // {
    //     _playerController.Move(PlayerInputDirection.LeftDown);
    // }
    //
    // public void OnKeyMoveS()
    // {
    //     _playerController.Move(PlayerInputDirection.RightDown);
    // }
    //
    // public void OnKeyMoveD()
    // {
    //     _playerController.Move(PlayerInputDirection.RightUp);
    // }
    //
    #endregion

    // public void OnLeftClick()
    // {
    //     Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
    //     if (hit.collider == null) return;
    //     //_mousePos = hit.point;
    //     _playerController.Move(_mousePos - transform.position);
    // }
    
    



}
