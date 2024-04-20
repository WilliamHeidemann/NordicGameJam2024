using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _rotateInput;
    private bool _boosting;
    private bool _holdingMirror;
    private Rigidbody2D _rigidbody2D;


    public float MoveSpeed = 10f;
    public float RotationSpeed = 100f;
    public float BoostRotationSpeed = 200f;

    public GameObject Mirror;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void ToggleMirror()
    {
        _holdingMirror = !_holdingMirror;
        Mirror.SetActive(_holdingMirror);
        if (_holdingMirror)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _moveInput = Vector2.zero;
        }
        else
        {
            _rotateInput = 0;
        }


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

    void FixedUpdate()
    {
        if (!_holdingMirror)
            Move();
    }

    void Update()
    {
        if (_holdingMirror)
            Rotate();
    }

    void Move()
    {
        _rigidbody2D.velocity = _moveInput * MoveSpeed;
    }

    void Rotate()
    {
        Debug.Log($"{_boosting}");
        var rotationSpeed = _boosting ? BoostRotationSpeed : RotationSpeed;
        transform.Rotate(0, 0, -_rotateInput * rotationSpeed * Time.deltaTime);
    }
}
