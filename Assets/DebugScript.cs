using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public Transform player;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position, transform.position);
        var direction = transform.position - player.position;
        var reflection = Vector3.Reflect(direction, transform.up);
        Gizmos.DrawLine(transform.position, transform.position + reflection);

    }
}
