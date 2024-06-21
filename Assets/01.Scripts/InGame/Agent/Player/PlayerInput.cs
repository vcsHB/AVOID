using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private Player _player;
    private PlayerController _playerController;
    
    private Vector3 _mousePos;
    private Vector3 _screenCenter;
    [SerializeField] private bool _isMouseUp;


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

}
