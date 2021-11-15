using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Xiaodt;
    private bool kaiGuan = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        kaiGuanXiaodt();
        Xiaodt.SetActive(kaiGuan);
    }

    private void kaiGuanXiaodt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            kaiGuan = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            kaiGuan = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            kaiGuan = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            kaiGuan = false;
        }
    }
}