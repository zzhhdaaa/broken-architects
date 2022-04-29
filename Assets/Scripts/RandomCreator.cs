using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreator : MonoBehaviour
{
    public GameObject target;
    public float offset;
    public float gap;
    private Vector3 initialTarget;
    private GameObject clickBrick;
    private Rigidbody rb;
    public float clickForce = 0.5f;

    [HideInInspector]
    public int clickTimes = -1;

    private void Start()
    {
        if (CursorManager.Instance != null)
        {
        CursorManager.Instance.OnBrickClicked += ClickBrick;//订阅手柄点击事件
        }
        if (MouseManager.Instance != null)
        {
        MouseManager.Instance.OnBrickClicked += ClickBrick;//订阅鼠标点击事件
        }
        initialTarget = target.transform.position;
    }

    private void Update()
    {
    }

    public void ClickBrick(GameObject brick)
    {
        if (brick != null)
        {
            clickTimes += 1;
            clickBrick = brick;
            rb = clickBrick.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, clickForce);
            if (clickBrick.GetComponent<MoveToRandomTarget>() != null)
            {
                if (!clickBrick.GetComponent<MoveToRandomTarget>().settled)
                {
                    target.transform.position = new Vector3(initialTarget.x + Random.Range(-offset, offset), gap * clickTimes, initialTarget.z + Random.Range(-offset, offset));
                    target.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
                    clickBrick.GetComponent<MoveToRandomTarget>().targetPos = target.transform.position;
                    clickBrick.GetComponent<MoveToRandomTarget>().targetRot = target.transform.rotation;
                    clickBrick.GetComponent<MoveToRandomTarget>().flyingOut = true;
                }
                else
                {
                    clickBrick.GetComponent<MoveToRandomTarget>().targetPos = clickBrick.GetComponent<MoveToRandomTarget>().startPos;
                    clickBrick.GetComponent<MoveToRandomTarget>().targetRot = clickBrick.GetComponent<MoveToRandomTarget>().startRot;
                    clickBrick.GetComponent<MoveToRandomTarget>().settled = false;
                    clickBrick.GetComponent<MoveToRandomTarget>().flyingOut = true;
                }
            }
        }
    }
}