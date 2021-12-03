using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Material[] startMaterials;
    public Material hoverMaterial;
    private Material[] hoverMaterials;
    bool mouseOver = false;


    private void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            startMaterials = GetComponent<Renderer>().materials;
            hoverMaterials = new Material[startMaterials.Length + 1];
            for (int i = 0; i < hoverMaterials.Length; i++)
            {
                hoverMaterials[i] = hoverMaterial;
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        GetComponent<Renderer>().materials = hoverMaterials;
    }


    private void OnMouseExit()

    {
        mouseOver = false;
        GetComponent<Renderer>().materials = startMaterials;
    }
}
