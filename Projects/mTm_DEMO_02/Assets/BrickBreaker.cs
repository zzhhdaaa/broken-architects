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
    public GameObject st;
    public float speedslow = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        MouseManager.Instance.OnBrickClicked += DestroyBrick;
    }

    // Update is called once per frame
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
        }
        st.GetComponent<LetCubeFly>().Fly(speedslow);
    }
}
