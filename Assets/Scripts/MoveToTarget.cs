using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveToTarget : MonoBehaviour
{
    public GameObject target;
    [HideInInspector]
    public bool flyingOut = false;//飞出去了没？
    [HideInInspector]
    public bool settled = false;//是否完全到达？
    private float stoppingDistance = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        MoveToTargetAndSettle();
    }

    void MoveToTargetAndSettle()
    {
        //如果飞出去 且 没到
        if (flyingOut && !settled)
        {
            //先按照物理飞一小段
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 3.0f)
                {
                    Destroy(gameObject.GetComponent<Rigidbody>());
                }
            }
            //再按照Lerp飞到目的地
            else
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, Time.deltaTime);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.transform.rotation, Time.deltaTime);
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) < stoppingDistance)
                {
                    target.SetActive(false);
                    gameObject.transform.position = target.transform.position;
                    gameObject.transform.rotation = target.transform.rotation;
                    settled = true;
                }
            }
        }
        else
        {
            return;
        }
    }
}
