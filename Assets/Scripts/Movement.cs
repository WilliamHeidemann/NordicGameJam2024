using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _rotateInput;
    private bool _boosting;

    private bool _holdingMirror;

    public float MoveSpeed = 10f;
    public float RotationSpeed = 100f;
    public float BoostRotationSpeed = 200f;

    public GameObject Mirror;

    void ToggleMirror()
    {
        _holdingMirror = !_holdingMirror;
        Mirror.SetActive(_holdingMirror);
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnRotate(InputValue value)
    {
        _rotateInput = value.Get<float>();
    }

    void OnBoost(InputValue value)
    {
        _boosting = value.isPressed;
    }

    void OnMirror()
    {
        ToggleMirror();
    }

    void Update()
    {
        if (_holdingMirror)
            Rotate();
        else
            Move();
    }

    void Move()
    {
        transform.position += MoveSpeed * Time.deltaTime * (Vector3)_moveInput;
    }

    void Rotate()
    {
        var rotationSpeed = _boosting ? BoostRotationSpeed : RotationSpeed;
        transform.Rotate(0, 0, -_rotateInput * rotationSpeed * Time.deltaTime);
    }

}
