using DG.Tweening;
using SoundManage;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro _optionText;
    // ======
    
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _moveCell = 20f;
    [SerializeField] private LayerMask _slotLayer;
    private bool _isMoving;
    [SerializeField]
    private OptionObject _currentOption;
    private MeshRenderer _selectBoxMesh;
    private int _selectBoxColorHash;
    private SoundObject _soundObject;

    private void Awake()
    {
        _soundObject = GetComponent<SoundObject>();
        _selectBoxMesh = transform.Find("Visual").GetComponent<MeshRenderer>();
        _selectBoxColorHash = Shader.PropertyToID("_Color");
        Initialize();
    }

    #region Input System
    
    public void OnMove(InputValue inputValue)
    {
        if (_isMoving || !TitleSceneManager.Instance.canControl) return;
        Vector3 direction = inputValue.Get<Vector3>();
        if (MoveCheck(direction))
        {
            MoveControl();
        }
    }
    
    public void OnEnter()
    {
        _currentOption.Select();
    }

    #endregion

    private void Initialize()
    {
        Time.timeScale = 1;
    }
    

    private bool MoveCheck(Vector3 direction)
    {
        Physics.Raycast(transform.position, direction, out RaycastHit hit, _detectDistance, _slotLayer);
        if (hit.collider == null) return false;

        if (hit.transform.TryGetComponent(out OptionObject option))
        {
            _currentOption = option;
            
            return true;
        }

        return false;
    }

   
    
    public void MoveControl()
    {
        _isMoving = true;
        _soundObject.Play(0);
        transform.DOMove(_currentOption.transform.position, _moveDuration).SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                _isMoving = false;
                _optionText.text = _currentOption.optionName;
                _selectBoxMesh.material.SetColor(_selectBoxColorHash, _currentOption.optionColor);
            });

    }
}
