using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBox : MonoBehaviour
{
    public float PushingForce = .25f;
    public Vector2 LastMovedDirection;
    public float MaxDistance = 1f;

    public HashSet<(float, Moveable)> moveables = new();

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.P) && LastMovedDirection != Vector2.zero) // && wasd bliver brugt
        {
            if (!moveables.Any()) return;
            var (distance, movable) = moveables.ToList().OrderBy(pair => pair.Item1).First();
            if (distance < 0.5f) return;
            movable.transform.position = Vector2.MoveTowards(movable.transform.position, transform.position, 0.2f);
        }
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
