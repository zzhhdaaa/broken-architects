using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] centers;
    Vector3 startingPos;
    Quaternion startingRot;
    Vector3 targetPos;
    Quaternion targetRot;
    public float rotateSpeed = 30.0f;
    public float stopDistance = 3.0f;
    int centerNumber = 0;

    bool movingCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraView();
        RotateCamera();
    }

    void ChangeCameraView()
    {
        if (movingCamera)
        {
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("AnLe1");
                movingCamera = true;
                centerNumber = 1;
                targetPos = cameras[0].transform.position;
                targetRot = cameras[0].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("AnLe2");
                movingCamera = true;
                centerNumber = 2;
                targetPos = cameras[1].transform.position;
                targetRot = cameras[1].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("AnLe3");
                movingCamera = true;
                centerNumber = 3;
                targetPos = cameras[2].transform.position;
                targetRot = cameras[2].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 30;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("AnLe4");
                movingCamera = true;
                centerNumber = 4;
                targetPos = cameras[3].transform.position;
                targetRot = cameras[3].transform.rotation;
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = 50;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("AnLe0");
                movingCamera = true;
                centerNumber = 0;
                targetPos = startingPos;
                targetRot = startingRot;
                Camera.main.orthographic = true;
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
                Debug.Log("AnLeA");
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("AnLeD");
                transform.RotateAround(centers[centerNumber].transform.position, new Vector3(0, 1, 0), -rotateSpeed * Time.deltaTime);
            }
        }
    }
}
