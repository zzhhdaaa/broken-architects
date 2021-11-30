using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class RoofFlyer : MonoBehaviour
{
    private GameObject flyingRoof;
    private Rigidbody rb;
    public float flyForce = 0.5f;

    void Start()
    {
        MouseManager.Instance.OnRoofClicked += LetRoofFly;//订阅鼠标点击事件
    }

    void Update()
    {

    }

    public void LetRoofFly(GameObject roof)
    {
        flyingRoof = roof;
        rb = flyingRoof.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, flyForce, rb.velocity.z);
    }
}
