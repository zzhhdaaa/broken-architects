using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class BrickBreaker : MonoBehaviour
{
    private GameObject destroyBrick;
    private Rigidbody rb;
    public float destroyForce = 0.5f;

    void Start()
    {
        MouseManager.Instance.OnBrickClicked += DestroyBrick;//订阅鼠标点击事件
    }

    void Update()
    {

    }

    public void DestroyBrick(GameObject brick)
    {
        if (brick != null)
        {
            destroyBrick = brick;
            rb = destroyBrick.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, destroyForce);
            if (destroyBrick.GetComponent<MoveToTarget>() != null)
            {
                destroyBrick.GetComponent<MoveToTarget>().flyingOut = true;
            }
        }
    }
}
