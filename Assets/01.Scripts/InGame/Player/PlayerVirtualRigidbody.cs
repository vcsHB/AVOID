using UnityEngine;

public class PlayerVirtualRigidbody : VirtualRigidbody
{
    [SerializeField] private float _moveCell = 2.5f;
    public static Vector3 PlayerGravity = new Vector3(0, -9.8f, 0);
    
    
}