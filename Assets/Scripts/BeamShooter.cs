using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShooter : MonoBehaviour
{

    [SerializeField] private LineRenderer lineBeforeGlass;
    [SerializeField] private LineRenderer lineAfterGlass;
    private Beam beam = new();
    [SerializeField] private Color colorBeforeGlassStart;
    [SerializeField] private Color colorBeforeGlassEnd;
    [SerializeField] private Color colorAfterGlassStart;
    [SerializeField] private Color colorAfterGlassEnd;

    private void Awake()
    {
        lineBeforeGlass.startWidth = lineBeforeGlass.endWidth = lineAfterGlass.startWidth = lineAfterGlass.endWidth = 0.3f;
        lineBeforeGlass.startColor = colorBeforeGlassStart;
        lineBeforeGlass.endColor = colorBeforeGlassEnd;
        lineAfterGlass.startColor = colorAfterGlassStart;
        lineAfterGlass.endColor = colorAfterGlassEnd;
    }

    private void FixedUpdate()
    {
        beam.ResetPoints();
        beam.CalculatePoints(transform.position, transform.position, transform.up, false);
        lineBeforeGlass.positionCount = beam.HitsBeforeGlass.Count;
        lineBeforeGlass.SetPositions(beam.HitsBeforeGlass.ToArray());
        lineAfterGlass.positionCount = beam.HitsAfterGlass.Count;
        lineAfterGlass.SetPositions(beam.HitsAfterGlass.ToArray());
    }
}

public class Beam
{
    public readonly List<Vector3> HitsBeforeGlass = new();
    public readonly List<Vector3> HitsAfterGlass = new();

    public void ResetPoints()
    {
        HitsBeforeGlass.Clear();
        HitsAfterGlass.Clear();
    }

    public void CalculatePoints(Vector2 rayStart, Vector2 lineStart, Vector2 direction, bool wentThroughGlass)
    {
        var list = wentThroughGlass ? HitsAfterGlass : HitsBeforeGlass;
        list.Add(lineStart);
        if (list.Count >= 5) return;

        var raycast = Physics2D.Raycast(rayStart, direction);
        var point = raycast.point;

        if (raycast.collider is null) return;
        if (raycast.collider.CompareTag("Mirror"))
        {
            var angledDirection = Vector2.Reflect(direction, raycast.transform.up);
            CalculatePoints(point + angledDirection, point, angledDirection, wentThroughGlass);
            return;
        }

        if (raycast.collider.CompareTag("Glass") && !wentThroughGlass)
        {
            CalculatePoints(point + direction, point, direction, true);
        }

        if (wentThroughGlass && raycast.transform.TryGetComponent<Bonfire>(out var bonfire))
        {
            bonfire.LightUp();
        }

        list.Add(raycast.point + (direction * 0.5f));
    }
}