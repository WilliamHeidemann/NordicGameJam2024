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
        lineRenderer.startWidth = lineRenderer.endWidth = 0.2f;
    }

    void Update()
    {
        var points = new List<Vector3>() { transform.position };
        points = GetPoints(points, transform.position, transform.up);
        lineRenderer.positionCount = points.Count;
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
            var angledDirection = CalculateDirection(direction, raycast.transform.up);
            return GetPoints(points, point, angledDirection);
        }

        return points;
    }

    private Vector3 CalculateDirection(Vector3 direction, Vector3 normal)
    {
        return Vector3.Reflect(direction, normal);
    }
}
