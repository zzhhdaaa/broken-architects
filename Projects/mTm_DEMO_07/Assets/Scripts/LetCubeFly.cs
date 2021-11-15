using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetCubeFly : MonoBehaviour
{
    public float speed = 10.0f;

    void Start()
    {
        Fly(speed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fly(float s)
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, s, 0);
    }
}
