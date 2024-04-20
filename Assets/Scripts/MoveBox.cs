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
    private float MaxDistance = 3f;
    public List<Movable> movables;

    private void Start()
    {
        movables = FindObjectsByType<Movable>(FindObjectsSortMode.None).ToList();
    }

    private void FixedUpdate()
    {
        print(movables.Count);
        if (Input.GetKey(KeyCode.P) && LastMovedDirection != Vector2.zero)
        {
            if (!movables.Any()) return;
            var closest = movables.OrderBy(movable => Vector3.Distance(movable.transform.position, transform.position)).First();
            var distance = Vector3.Distance(closest.transform.position, transform.position);
            if (distance > MaxDistance) return;
            if (distance < 0.5f) return;
            closest.transform.position = Vector2.MoveTowards(closest.transform.position, transform.position, 0.2f);
            print("Moving");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(PushingForce * LastMovedDirection);
        }
    }
}
