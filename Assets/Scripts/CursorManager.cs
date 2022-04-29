using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public event Action<GameObject> OnBrickClicked;

    public event Action<GameObject> OnRoofClicked;

    public float Speed = 10.0f;

    [HideInInspector]
    public RectTransform rect;
    private RaycastHit hitInfo;

    private Image image;
    public Sprite[] cursorImage;
    public float cursorTime = 0.6f;
    private float timmer = 0.0f;
    private bool timmerOn = false;

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

    // Start is called before the first frame update
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveCursorAndClick();
        SetCursor();
    }

    private void MoveCursorAndClick()
    {
        //get input
        Vector2 joy = new Vector2(Input.GetAxis("LeftJoyX"), -Input.GetAxis("LeftJoyY"));
        //if (joy.magnitude < 0.3f) { return; }
        joy.Normalize();

        //local variables
        float width = Screen.width;
        float height = Screen.height;
        float multiplier = Speed * Time.deltaTime;
        Vector2 anchor = rect.anchoredPosition;

        //update values
        float x = anchor.x + joy.x * multiplier;
        x = Mathf.Clamp(x, -width / 2, width / 2);
        float y = anchor.y + joy.y * multiplier;
        y = Mathf.Clamp(y, -height / 2, height / 2);

        //set anchor
        anchor = new Vector2(x, y);
        rect.anchoredPosition = anchor;

        //click
        Ray ray = Camera.main.ScreenPointToRay(rect.position);
        if (Physics.Raycast(ray, out hitInfo))
        {
            switch (hitInfo.collider.tag)
            {
                case "Bricks":
                    if (Input.GetButtonDown("West") && hitInfo.collider != null)
                    {
                        OnBrickClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    else if (Input.GetButtonDown("South") && hitInfo.collider != null)
                    {
                        OnBrickClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    else if (Input.GetButtonDown("East") && hitInfo.collider != null)
                    {
                        OnBrickClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    break;

                case "Roofs":
                    if (Input.GetButtonDown("West") && hitInfo.collider != null)
                    {
                        OnRoofClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    else if (Input.GetButtonDown("South") && hitInfo.collider != null)
                    {
                        OnRoofClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    else if (Input.GetButtonDown("East") && hitInfo.collider != null)
                    {
                        OnRoofClicked?.Invoke(hitInfo.collider.gameObject);
                    }
                    break;
            }
        }
    }

    private void SetCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(rect.position);
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
                image.sprite = cursorImage[0];
                //Cursor.SetCursor(cursorImage[0], new Vector2(32, 32), CursorMode.ForceSoftware);
            }
            else
            {
                if (Input.GetButtonDown("West") && hitInfo.collider != null)
                {
                    image.sprite = cursorImage[2];
                    //Cursor.SetCursor(cursors[2], new Vector2(32, 32), CursorMode.ForceSoftware);
                    timmerOn = true;
                }
                else if (Input.GetButtonDown("East") && hitInfo.collider != null)
                {
                    image.sprite = cursorImage[2];
                    //Cursor.SetCursor(cursors[2], new Vector2(32, 32), CursorMode.ForceSoftware);
                    timmerOn = true;
                }
                else if (Input.GetButtonDown("South") && hitInfo.collider != null)
                {
                    image.sprite = cursorImage[2];
                    //Cursor.SetCursor(cursors[2], new Vector2(32, 32), CursorMode.ForceSoftware);
                    timmerOn = true;
                }
                else
                {
                    image.sprite = cursorImage[1];
                    //Cursor.SetCursor(cursors[1], new Vector2(32, 32), CursorMode.ForceSoftware);
                }
            }
        }
    }
}