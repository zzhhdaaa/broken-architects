using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    public event Action<GameObject> OnBrickClicked;
    public event Action<GameObject> OnRoofClicked;
    public Texture2D cursor01;
    public Texture2D cursor02;
    public Texture2D cursor03;
    public Texture2D cursor04;
    public Texture2D cursor05;
    public float shiChang = 0.6f;
    private float jiShi = 0.0f;
    private bool kaiGuan = false;

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

        //Debug.Log(AddAge(5, 21));

    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();
        SetCursor();
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
                        Debug.Log("Hit Bricks");
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

    /*public int AddAge(int a, int b)
    {
        int c = a + b;
        return c;
    }*/

    /*public void AddAndPrint(int a, int b)
    {
        int c = a + b;
        Debug.Log(c);
    }*/

    void SetCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (kaiGuan)
        {
            if (jiShi < shiChang)
            {
                jiShi += Time.deltaTime;
            }
            else
            {
                kaiGuan = false;
                jiShi = 0.0f;
            }
        }
        else
        {
            if (!Physics.Raycast(ray, out hitInfo) || hitInfo.collider.tag != "Bricks")
            {
                Cursor.SetCursor(cursor01, new Vector2(32, 32), CursorMode.ForceSoftware);
            }
            else
            {
                if (!Input.GetMouseButtonDown(0) || hitInfo.collider == null)
                {
                    Cursor.SetCursor(cursor02, new Vector2(32, 32), CursorMode.ForceSoftware);
                }
                else
                {
                    Debug.Log("DianLe");
                    Cursor.SetCursor(cursor03, new Vector2(32, 32), CursorMode.ForceSoftware);
                    kaiGuan = true;
                }
            }
        }
    }
}
