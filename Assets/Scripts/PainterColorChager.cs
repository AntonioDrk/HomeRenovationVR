using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterColorChager : MonoBehaviour
{
    [SerializeField] private Color paintColor = Color.cyan;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer.material.color = paintColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check to see if it's a paint roller
        if (other.transform.CompareTag("PaintBrush"))
        {
            Debug.Log("BUCKET collided with " + other.transform.name);
            other.transform.parent.GetComponent<Painter>().SetColor(paintColor);
        }
    }
}
