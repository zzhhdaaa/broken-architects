using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraRandom : MonoBehaviour
{
    public GameObject[] cameras;//各个机位
    public GameObject[] centers;//旋转中心
    private Vector3 startingPos, targetPos;
    private Quaternion startingRot, targetRot;
    public float rotateSpeed = 30.0f;//旋转速度
    public float stopDistance = 3.0f;//Lerp的截止距离
    private bool fullScreen = false, movingCamera = false;//全屏吗？相机在移动吗？
    private int cameraIndex = 0;

    [HideInInspector]
    public int cameraTrueIndex = 5;

    public AllRandomSettle allRandomSettle;//全部点击命中会触发一次强制相机位移

    [HideInInspector]
    public int centerNumber = 0;//当前机位和转轴编号

    private void Start()
    {
        allRandomSettle = GameObject.FindObjectOfType<AllRandomSettle>();
        startingPos = transform.position;
        startingRot = transform.rotation;
    }

    private void Update()
    {
        ChangeCameraIndex();
        ChangeCameraView();
        RotateCamera();
        ChangeScreenMode();
    }

    private void ChangeCameraView()
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
                cameraIndex = 0;
            }
        }
        else
        {
            //按下12340的时候分别设置编号和触发相机移动
            if (Input.GetKeyDown(KeyCode.Alpha1) || cameraIndex == 1)
            {
                centerNumber = 1;
                targetPos = cameras[0].transform.position;
                targetRot = cameras[0].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || cameraIndex == 2)
            {
                centerNumber = 2;
                targetPos = cameras[1].transform.position;
                targetRot = cameras[1].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || cameraIndex == 3)
            {
                centerNumber = 3;
                targetPos = cameras[2].transform.position;
                targetRot = cameras[2].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
                movingCamera = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || cameraIndex == 4 || allRandomSettle.cameraMoveAfterSettled)
            {
                centerNumber = 4;
                targetPos = cameras[3].transform.position;
                targetRot = cameras[3].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 50;
                movingCamera = true;
                cameraTrueIndex = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0) || cameraIndex == 5)
            {
                centerNumber = 0;
                targetPos = startingPos;
                targetRot = startingRot;
                Camera.main.orthographic = true;
                movingCamera = true;
            }
        }
    }

    private void ChangeCameraIndex()
    {
        if (!movingCamera)
        {
            if (Input.GetButtonDown("LeftBumper") || Input.GetAxis("LeftTrigger") != 0f || Input.GetKeyDown(KeyCode.W))
            {
                cameraTrueIndex -= 1;
                if (cameraTrueIndex == 0)
                {
                    cameraTrueIndex = 5;
                }
                cameraIndex = cameraTrueIndex;
            }
            else if (Input.GetButtonDown("RightBumper") || Input.GetAxis("RightTrigger") != 0f || Input.GetKeyDown(KeyCode.S))
            {
                cameraTrueIndex += 1;
                if (cameraTrueIndex == 6)
                {
                    cameraTrueIndex = 1;
                }
                cameraIndex = cameraTrueIndex;
            }
        }
    }

    private void RotateCamera()
    {
        if (movingCamera)
        {
            return;
        }
        else
        {
            if (Input.GetAxis("RightJoyX") < 0f || Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetAxis("RightJoyX") > 0f || Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), -rotateSpeed * Time.deltaTime);
            }
        }
    }

    private void ChangeScreenMode()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fullscreen"))
        {
            fullScreen = !fullScreen;
        }
        else if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload"))
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