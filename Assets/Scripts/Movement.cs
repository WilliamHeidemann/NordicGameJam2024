using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(MoveBox), typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    public Sprite MirrorSprite;
    public Sprite NormalSprite;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private float _rotateInput;
    private bool _boosting;
    private bool _holdingMirror;
    private Rigidbody2D _rigidbody2D;
    public SpriteRenderer MySpriteRenderer;
    private Quaternion _lastRotationWithMirror;

    private MoveBox _moveBox;

    public float MoveSpeed = 10f;
    public float RotationSpeed = 100f;
    public float BoostRotationSpeed = 200f;

    public GameObject Mirror;

    [SerializeField] private AudioSource walkingAudio;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moveBox = GetComponent<MoveBox>();
        _playerInput = GetComponent<PlayerInput>();
        walkingAudio.volume = 0;
    }

    void ToggleMirror()
    {
        _holdingMirror = !_holdingMirror;
        Mirror.SetActive(_holdingMirror);
        MySpriteRenderer.flipY = _holdingMirror;

        if (_holdingMirror)
        {
            // pulled up mirror
            MySpriteRenderer.sprite = MirrorSprite;

            _playerInput.SwitchCurrentActionMap("Mirror Mode");
            _rotateInput = 0;
            transform.rotation = _lastRotationWithMirror;
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            // took away mirror
            MySpriteRenderer.sprite = NormalSprite;
            _playerInput.SwitchCurrentActionMap("Movement");
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
        walkingAudio.volume = _moveInput != Vector2.zero ? 1 : 0;
        _rigidbody2D.velocity = _moveInput * MoveSpeed;
        _moveBox.LastMovedDirection = _moveInput.normalized;
    }

    void Rotate()
    {
        if (_rotateInput == 0)
            return;

        var rotationSpeed = _boosting ? BoostRotationSpeed : RotationSpeed;
        var rotationAmount = -_rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);
    }
}
