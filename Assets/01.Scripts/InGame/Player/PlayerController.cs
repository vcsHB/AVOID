using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private bool _isStun;
    
    public void OnMouse(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        print(mousePos);
        
    }
    
    
}
