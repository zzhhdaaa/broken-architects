using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    public event Action<GameObject> OnBrickClicked;
    public event Action<GameObject> OnRoofClicked;

    public Texture2D[] cursors;
    public float cursorTime = 0.6f;
    private float timmer = 0.0f;
    private bool timmerOn = false;

    private RaycastHit hitInfo;

    // Start is called before the first frame update
    private void Awake()
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
    private void Update()
    {
        MouseControl();
        SetCursor();
    }

    private void MouseControl()
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
                    }
                    break;
                case "Roofs":
                    if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
                    {
                        OnRoofClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    break;
            }
        }
    }

    private void SetCursor()//设置光标的样式
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (timmerOn)
        {
            if (timmer < cursorTime)
            {
                timmer += Time.deltaTime;
            }
            else
            {
                timmerOn = false;
                timmer = 0.0f;
            }
        }
        else
        {
            if (!Physics.Raycast(ray, out hitInfo) || hitInfo.collider.tag != "Bricks")
            {
                Cursor.SetCursor(cursors[0], new Vector2(32, 32), CursorMode.ForceSoftware);
            }
            else
            {
                if (!Input.GetMouseButtonDown(0) || hitInfo.collider == null)
                {
                    Cursor.SetCursor(cursors[1], new Vector2(32, 32), CursorMode.ForceSoftware);
                }
                else
                {
                    Cursor.SetCursor(cursors[2], new Vector2(32, 32), CursorMode.ForceSoftware);
                    timmerOn = true;
                }
            }
        }
    }
}