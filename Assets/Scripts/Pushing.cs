using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    public float PushingForce = 5f;
    public Vector2 LastMovedDirection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(PushingForce * LastMovedDirection);
        }
    }
}
