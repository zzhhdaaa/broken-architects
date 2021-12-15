using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Material[] startMaterials;
    public Material hoverMaterial;
    private Material[] hoverMaterials;
    private CursorManager cursorManager;
    private RaycastHit hitInfo;

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
        cursorManager = FindObjectOfType<CursorManager>();
    }

    private void Update()
    {
        ChangeTheColor();
    }

    /*private void OnMouseEnter()
    {
        GetComponent<Renderer>().materials = hoverMaterials;
    }


    private void OnMouseExit()

    {
        GetComponent<Renderer>().materials = startMaterials;
    }*/

    private void ChangeTheColor()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursorManager.rect.position);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider == this.GetComponent<BoxCollider>() || hitInfo.collider == this.GetComponent<MeshCollider>())
            {
                GetComponent<Renderer>().materials = hoverMaterials;
            }
            else
            {
                GetComponent<Renderer>().materials = startMaterials;
            }
        }
    }
}
