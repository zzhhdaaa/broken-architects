using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    public event Action<GameObject> OnBrickClicked;
    public event Action<GameObject> OnRoofClicked;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();
    }

    void MouseControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            switch (hitInfo.collider.tag)
            {
                case "Bricks":
                    if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
                    {
                        OnBrickClicked?.Invoke(hitInfo.collider.gameObject);
                        Debug.Log("Hit Bricks");
                    }
                    break;
                case "Roofs":
                    if (Input.GetMouseButtonDown(1) && hitInfo.collider != null)
                    {
                        OnRoofClicked?.Invoke(hitInfo.collider.gameObject);
                        Debug.Log("Hit Roofs");
                    }
                    break;
            }
        }
    }
}
