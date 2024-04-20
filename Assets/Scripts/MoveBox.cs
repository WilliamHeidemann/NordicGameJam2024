using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBox : MonoBehaviour
{
    public float PushingForce = .25f;
    public Vector2 LastMovedDirection;
    public float MaxDistance = 5;

    public HashSet<(float, Moveable)> moveables;

    private void Awake()
    {
        moveables = new();
    }

    private void Update()
    {
    }

    public void AddToMoveables(float distance, Moveable moveable)
    {
        moveables.Add((distance, moveable));
    }

    public void RemoveFromMoveables(float distance, Moveable moveable)
    {
        moveables.Remove((distance, moveable));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(PushingForce * LastMovedDirection);
        }
    }
}
