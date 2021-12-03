using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRandomTarget : MonoBehaviour
{
    [HideInInspector]
    public bool flyingOut = false;//飞出去了没？
    [HideInInspector]
    public bool settled = false;//是否完全到达？
    private float stoppingDistance = 1.0f;

    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Quaternion startRot;

    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public Quaternion targetRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
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
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRot, Time.deltaTime);
                if (Vector3.Distance(gameObject.transform.position, targetPos) < stoppingDistance)
                {
                    gameObject.transform.position = targetPos;
                    gameObject.transform.rotation = targetRot;
                    flyingOut = false;
                    if (targetPos == startPos)
                    {
                        settled = false;
                        gameObject.AddComponent<Rigidbody>();
                        //gameObject.GetComponent<Rigidbody>().drag = 5f;
                    }
                    else
                    {
                        settled = true;
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
}
