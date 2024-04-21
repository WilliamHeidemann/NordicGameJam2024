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
    private float MaxDistance = 1.2f;
    public List<Movable> movables;
    private bool _pulling;
    private Movable _pullTarget;
    private float MinimumPullingDistance = .9f;
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
                _pullTarget.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Prompt.Instance.HideE();
            }
        }
        else
        {
            _pullTarget.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
        else
        {
            var closest = GetClosestMovable();
            if (closest == null) Prompt.Instance.HideE();
            else Prompt.Instance.ShowE(closest.transform.position + Vector3.up);
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

}
