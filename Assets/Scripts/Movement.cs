using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Pushing))]
public class Movement : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _rotateInput;
    private bool _boosting;
    private bool _holdingMirror;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _lastRotationWithMirror;

    private Pushing _pushing;

    public float MoveSpeed = 10f;
    public float RotationSpeed = 100f;
    public float BoostRotationSpeed = 200f;

    public GameObject Mirror;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pushing = GetComponent<Pushing>();
    }

    void ToggleMirror()
    {
        _holdingMirror = !_holdingMirror;
        Mirror.SetActive(_holdingMirror);
        if (_holdingMirror)
        {
            // pulled up mirror
            transform.rotation = _lastRotationWithMirror;
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            // took away mirror
            _lastRotationWithMirror = transform.rotation;
            transform.rotation = Quaternion.identity;
        }
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnRotate(InputValue value)
    {
        if (_holdingMirror)
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
        if (_moveInput.magnitude == 0)
            return;

        _rigidbody2D.velocity = _moveInput * MoveSpeed;
        _pushing.LastMovedDirection = _moveInput;
        _pushing.LastMovedDirection.Normalize();
    }

    void Rotate()
    {
        var rotationSpeed = _boosting ? BoostRotationSpeed : RotationSpeed;
        var rotationAmount = -_rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);
    }
}
