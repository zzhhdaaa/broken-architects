using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;//各个机位
    public GameObject[] centers;//旋转中心
    private Vector3 startingPos, targetPos;
    private Quaternion startingRot, targetRot;
    public float rotateSpeed = 30.0f;//旋转速度
    public float stopDistance = 3.0f;//Lerp的截止距离
    private bool fullScreen = false, movingCamera = false;//全屏吗？相机在移动吗？

    public AllSettle allSettle;//全部点击命中会触发一次强制相机位移

    [HideInInspector]
    public int centerNumber = 0;//当前机位和转轴编号

    void Start()
    {
        allSettle = GameObject.FindObjectOfType<AllSettle>();
        startingPos = transform.position;
        startingRot = transform.rotation;
    }

    void Update()
    {
        ChangeCameraView();
        RotateCamera();
        ChangeScreenMode();
    }

    void ChangeCameraView()
    {
        if (movingCamera)
        {
            //Lerp的固定操作
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 3.0f);
            if (Vector3.Distance(transform.position, targetPos) <= stopDistance)
            {
                transform.position = targetPos;
                transform.rotation = targetRot;
                movingCamera = false;
            }
        }
        else
        {
            //按下12340的时候分别设置编号和触发相机移动
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                centerNumber = 1;
                targetPos = cameras[0].transform.position;
                targetRot = cameras[0].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                centerNumber = 2;
                targetPos = cameras[1].transform.position;
                targetRot = cameras[1].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                centerNumber = 3;
                targetPos = cameras[2].transform.position;
                targetRot = cameras[2].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || allSettle.cameraMoveAfterSettled)
            {
                centerNumber = 4;
                targetPos = cameras[3].transform.position;
                targetRot = cameras[3].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 50;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                centerNumber = 0;
                targetPos = startingPos;
                targetRot = startingRot;
                Camera.main.orthographic = true;
                movingCamera = true;
            }
        }
    }

    void RotateCamera()
    {
        if (movingCamera)
        {
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), -rotateSpeed * Time.deltaTime);
            }
        }
    }

    void ChangeScreenMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fullScreen = !fullScreen;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        }

        if (fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
