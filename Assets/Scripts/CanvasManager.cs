using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Xiaodt;
    private CameraRandom cameraRandom;
    private bool kaiGuan = false;

    // Start is called before the first frame update
    private void Start()
    {
        cameraRandom = FindObjectOfType<CameraRandom>();
    }

    // Update is called once per frame
    private void Update()
    {
        kaiGuanXiaodt();
        Xiaodt.SetActive(kaiGuan);
    }

    private void kaiGuanXiaodt()
    {
        if (cameraRandom.cameraTrueIndex == 1)
        {
            kaiGuan = true;
        }
        else if (cameraRandom.cameraTrueIndex == 2)
        {
            kaiGuan = true;
        }
        else if (cameraRandom.cameraTrueIndex == 3)
        {
            kaiGuan = true;
        }
        else if (cameraRandom.cameraTrueIndex == 4)
        {
            kaiGuan = true;
        }
        else if (cameraRandom.cameraTrueIndex == 5)
        {
            kaiGuan = false;
        }
    }
}