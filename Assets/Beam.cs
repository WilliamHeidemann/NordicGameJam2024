using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Beam : MonoBehaviour
{

    public LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        var points = new List<Vector3>() { transform.position };
        points = GetPoints(points, transform.position, transform.up);
        lineRenderer.SetPositions(points.ToArray());
    }

    private List<Vector3> GetPoints(List<Vector3> points, Vector3 start, Vector3 direction)
    {
        var raycast = Physics2D.Raycast(start, direction);
        var point = raycast.point;
        if (points.Count >= 5) return points;
        
        points.Add(point);

        
        if (raycast.collider.CompareTag("Mirror"))
        {
            var angledDirection = CalculateDirection(direction, raycast.transform.up.normalized);
            return GetPoints(points, point, angledDirection);
        }

        return points;
    }

    private Vector3 CalculateDirection(Vector3 direction, Vector3 normal)
    {
        return direction - 2 * Vector2.Dot(direction, normal) * normal;
    }
}
