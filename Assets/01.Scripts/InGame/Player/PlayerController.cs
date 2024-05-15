using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Agent
{
    private Vector3 mousePos;


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
    
    private void ChangeGravity()
    {
        
    }


    
}
