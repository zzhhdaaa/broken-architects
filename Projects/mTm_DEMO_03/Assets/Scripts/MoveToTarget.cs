using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveToTarget : MonoBehaviour
{
    public bool feiLe = false;
    public GameObject target;
    public bool tingLe = false;
    public bool daoLe = false;
    public bool wanShirLe = false;
    public float tingXia = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ZhongShenGuiWei();
    }

    void ZhongShenGuiWei()
    {
        if (feiLe && !daoLe)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < tingXia)
                {
                    tingLe = true;
                    Destroy(gameObject.GetComponent<Rigidbody>());
                }
            }
            else
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, Time.deltaTime);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.transform.rotation, Time.deltaTime);
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) < 1.0f)
                {
                    target.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.transform.position = target.transform.position;
                    gameObject.transform.rotation = target.transform.rotation;
                    daoLe = true;
                }
            }
        }
        else
        {
            if (daoLe && !wanShirLe)
            {
                //gameObject.AddComponent<Rigidbody>();
                wanShirLe = true;
            }
            else
            {
                return;
            }
        }
    }
}
