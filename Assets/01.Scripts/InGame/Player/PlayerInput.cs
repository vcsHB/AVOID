using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

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
        print(_mousePos);
    }

    public void OnLeftClick()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.collider == null) return;
        //_mousePos = hit.point;
        _playerController.Move(_mousePos - transform.position);
    }
    
    public void OnMove(InputValue value)
    {
        Vector3 dir = value.Get<Vector3>();
        _playerController.Move(dir);
    }



}
