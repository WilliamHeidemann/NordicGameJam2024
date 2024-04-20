using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Moveable : MonoBehaviour
{
    private MoveBox _moveBox;
    private SpriteRenderer _spriteRenderer;
    public Color CloseColor;
    private Color _defaultColor;

    private void Awake()
    {
        _moveBox = FindFirstObjectByType<MoveBox>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, _moveBox.transform.position);
        if (distanceToPlayer < _moveBox.MaxDistance)
        {
            _moveBox.AddToMoveables(distanceToPlayer, this);
            _spriteRenderer.color = CloseColor;
        }
        else
        {
            _moveBox.RemoveFromMoveables(distanceToPlayer, this);
            _spriteRenderer.color = _defaultColor;
        }
    }
}
