using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class CircleRenderer : MonoBehaviour
{
    private int vertexCount = 60; // Number of vertices for the circle

    private float lineWidth = 0.1f; // Width of the outline

    [SerializeField]
    private float radius = 2f; // Set this value according to your desired circle size

    private LineRenderer lineRenderer;

    private EdgeCollider2D edgeCollider;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        SetupCircle();
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = lineWidth;

        var deltaTheta = (2f * Mathf.PI) / vertexCount;
        var theta = 0f;

        Vector2[] colliderPoints = new Vector2[vertexCount];
        lineRenderer.positionCount = vertexCount;

        for (var i = 0; i < lineRenderer.positionCount; i++)
        {
            var pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            colliderPoints[i] = pos;
            theta += deltaTheta;
        }

        edgeCollider.points = colliderPoints;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var deltaTheta = (2f * Mathf.PI) / vertexCount;
        var theta = 0f;

        var oldPos = Vector3.zero;
        for (var i = 0; i <= vertexCount; i++)
        {
            var pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            oldPos = transform.position + pos;

            theta += deltaTheta;
        }
    }
#endif
}
