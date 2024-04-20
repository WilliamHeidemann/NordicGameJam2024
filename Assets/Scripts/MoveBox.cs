using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBox : MonoBehaviour
{
    public float PushingForce = .25f;
    public Vector2 LastMovedDirection;
    private float MaxDistance = 2f;
    public List<Movable> movables;
    private bool _pulling;
    private Movable _pullTarget;
    public float MinimumPullingDistance = 1.5f;
    public float PullingSpeed = 0.2f;

    private void Start()
    {
        movables = FindObjectsByType<Movable>(FindObjectsSortMode.None).ToList();
        _pulling = false;
    }

    private void OnPull()
    {
        if (!_pulling)
        {
            _pullTarget = GetClosestMovable();
            if (_pullTarget != null)
            {
                _pulling = true;
            }
        }
        else
        {
            _pullTarget = null;
            _pulling = false;
        }
    }

    private void Update()
    {
        if (_pulling)
        {
            var distance = Vector3.Distance(_pullTarget.transform.position, transform.position);

            if (distance < MinimumPullingDistance) return;
            _pullTarget.transform.position = Vector2.MoveTowards(_pullTarget.transform.position, transform.position, PullingSpeed);
        }
    }

    private Movable GetClosestMovable()
    {
        if (!movables.Any()) return null;
        var closest = movables.OrderBy(movable => Vector3.Distance(movable.transform.position, transform.position)).First();
        if (Vector3.Distance(closest.transform.position, transform.position) > MaxDistance)
            return null;
        else
            return closest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(PushingForce * LastMovedDirection);
        }
    }
}
