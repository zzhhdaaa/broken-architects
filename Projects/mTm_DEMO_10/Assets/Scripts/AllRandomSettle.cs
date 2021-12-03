using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRandomSettle : MonoBehaviour
{
    private MoveToRandomTarget[] moveToRandomTarget;//场景里所有MoveToTarget的Component
    private bool allSettled = false;
    public GameObject roof;//最后加盖一个屋顶？可以是空Object，仅用来存放动画
    public GameObject explosion;//动画
    private float offset = 5.0f;//动画向上偏移的量
    private float finalClickTimmer = 0.0f;//计时器，点击后过一会儿再爆炸


    [HideInInspector]
    public bool cameraMoveAfterSettled = false;
    private bool startConstruct = false;

    private RandomCreator randomCreator;

    public GameObject skyDome;
    public GameObject dirLight;
    public GameObject dirLight2;
    public GameObject lights;

    private void Start()
    {
        moveToRandomTarget = GameObject.FindObjectsOfType<MoveToRandomTarget>();//找到场景里所有MoveToTarget的Component
        randomCreator = FindObjectOfType<RandomCreator>();
    }

    private void Update()
    {
        FinishAndExplode();
    }

    private void FinishAndExplode()
    {
        if (Input.GetKeyDown(KeyCode.Return) || startConstruct)
        {

            if (finalClickTimmer <= 3.0f)
            {
                //相机移动，然后等待3秒让各项object落位
                cameraMoveAfterSettled = true;
                startConstruct = true;
                finalClickTimmer += Time.deltaTime;
                skyDome.SetActive(true);
                dirLight.SetActive(true);
                dirLight2.SetActive(true);
                dirLight.GetComponent<Light>().intensity = Mathf.Lerp(dirLight.GetComponent<Light>().intensity, 0.8f, Time.deltaTime);
                dirLight2.GetComponent<Light>().intensity = Mathf.Lerp(dirLight.GetComponent<Light>().intensity, 0.2f, Time.deltaTime);
            }
            else
            {
                //相机移动关闭
                cameraMoveAfterSettled = false;
                finalClickTimmer = 0f;

                //重新添加所有rigidbody
                for (int i = 0; i < moveToRandomTarget.Length; i++)
                {
                    if (moveToRandomTarget[i].settled)
                    {
                        moveToRandomTarget[i].gameObject.AddComponent<Rigidbody>();
                        moveToRandomTarget[i].gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    }
                }
                //爆炸特效
                Vector3 explosionPos = new Vector3(roof.transform.position.x, roof.transform.position.y - offset, roof.transform.position.z);
                GameObject obj = Instantiate(explosion, explosionPos, roof.transform.rotation);
                roof.SetActive(true);
                Debug.Log("GaiTMD!!");
                lights.SetActive(false);
                startConstruct = false;
                allSettled = true;
                //重置上升位置
                randomCreator.clickTimes = 1;
            }
        }


        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            int flewNumber = 0;//点击命中数量的计数器
            int settledNumber = 0;//到位数量的计数器

            //计算点击命中（flyingOut）的数量和到位的数量
            for (int i = 0; i < moveToRandomTarget.Length; i++)
            {
                if (moveToRandomTarget[i].flyingOut)
                {
                    flewNumber += 1;
                }
                if (moveToRandomTarget[i].settled)
                {
                    settledNumber += 1;
                }
            }

            //最后一下点击后，延时触发相机移动
            if (flewNumber >= moveToRandomTarget.Length)
            {
                if (finalClickTimmer <= 0.5f)
                {
                    finalClickTimmer += Time.deltaTime;
                }
                else
                {
                    cameraMoveAfterSettled = true;
                }
            }

            //全部到位后，延迟触发爆炸特效
            if (settledNumber >= moveToRandomTarget.Length)
            {
                if (allSettledTimmer <= 0.5f)
                {
                    allSettledTimmer += Time.deltaTime;
                }
                else
                {
                    //相机移动关闭
                    cameraMoveAfterSettled = false;

                    //重新添加所有Rigidbody
                    for (int i = 0; i < moveToRandomTarget.Length; i++)
                    {
                        moveToRandomTarget[i].gameObject.AddComponent<Rigidbody>();
                        moveToRandomTarget[i].gameObject.GetComponent<Rigidbody>().drag = 5;
                    }

                    //爆炸特效
                    Vector3 explosionPos = new Vector3(roof.transform.position.x, roof.transform.position.y - offset, roof.transform.position.z);
                    GameObject obj = Instantiate(explosion, explosionPos, roof.transform.rotation);
                    roof.SetActive(true);
                    Debug.Log("GaiTMD!!");
                    allSettled = true;
                }
            }
        }
        else
        {
            return;
        }*/
    }
}