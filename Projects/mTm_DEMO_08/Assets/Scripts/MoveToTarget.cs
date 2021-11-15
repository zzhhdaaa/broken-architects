using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveToTarget : MonoBehaviour
{
    public bool feiLe = false;
    public GameObject zhongShenGuiWei;
    public bool tingLe = false;
    public bool daoLe = false;
    public bool wanShirLe = false;
    public float tingXia = 10.0f;

    [HideInInspector]
    public float height;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        ZhongShenGuiWei();
    }

    private void ZhongShenGuiWei()
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
                gameObject.transform.position = Vector3.Lerp(transform.position, zhongShenGuiWei.GetComponent<ZhongShenGuiWei>().targetPos + new Vector3(0, height, 0), Time.deltaTime);
                gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, zhongShenGuiWei.GetComponent<ZhongShenGuiWei>().target.transform.rotation, Time.deltaTime);
                if (Vector3.Distance(gameObject.transform.position, zhongShenGuiWei.GetComponent<ZhongShenGuiWei>().targetPos + new Vector3(0, height, 0)) < 1.0f)
                {
                    //target.SetActive(false);
                    gameObject.transform.position = zhongShenGuiWei.GetComponent<ZhongShenGuiWei>().targetPos + new Vector3(0, height, 0);
                    gameObject.transform.rotation = zhongShenGuiWei.GetComponent<ZhongShenGuiWei>().target.transform.rotation;
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