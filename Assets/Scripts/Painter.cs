using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Painter : MonoBehaviour
{
    [SerializeField] private PaintingBehaviour paintObject;
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
        if (other.transform.CompareTag("PaintSurface"))
        {
            Debug.Log("COLLIDED WITH " + other.gameObject.name);
            //Vector3 avg = AverageOfContactPoints(other.contacts);

            //Debug.DrawRay(avg, other.contacts[0].normal * 100,Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 2f);
            foreach (ContactPoint point in other.contacts)
            {
                Debug.DrawRay(point.point, -point.normal * 3, Color.green, 1f);
                paintObject.ChangeBrushColor(paintingColor);
                paintObject.SpawnBrushPoint(point.point, -point.normal);  
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("PaintSurface"))
        {
            Vector3 avg = AverageOfContactPoints(other.contacts);
            paintObject.ChangeBrushColor(paintingColor);
            paintObject.SpawnBrushPoint(avg, -other.contacts[0].normal);
        }
    }

    public void SetColor(Color col)
    {
        paintingColor = col;
    }
}
