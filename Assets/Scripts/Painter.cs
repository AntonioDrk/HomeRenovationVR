using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Painter : MonoBehaviour
{
    [SerializeField] private PaintingBehaviour _paintObject;
    [SerializeField] private Color paintingColor;

    private Vector3 AverageOfContactPoints(ContactPoint[] contactPoints)
    {
        Vector3 averagePoint = Vector3.zero;
        
        foreach (ContactPoint contactPoint in contactPoints)
        {
            averagePoint += contactPoint.point;
        }

        averagePoint /= contactPoints.Length;

        return averagePoint;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLIDED WITH " + other.gameObject.name);

        Vector3 avg = AverageOfContactPoints(other.contacts);
        
        Debug.DrawRay(avg, other.contacts[0].normal * 100, 
            Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 25f);
        _paintObject.ChangeBrushColor(paintingColor);
        _paintObject.SpawnBrushPoint(avg, -other.contacts[0].normal);
    }
    
    private void OnCollisionStay(Collision other)
    {
        Vector3 avg = AverageOfContactPoints(other.contacts);
        _paintObject.ChangeBrushColor(paintingColor);
        _paintObject.SpawnBrushPoint(avg, -other.contacts[0].normal);
    }

    public void SetColor(Color col)
    {
        paintingColor = col;
    }
}
