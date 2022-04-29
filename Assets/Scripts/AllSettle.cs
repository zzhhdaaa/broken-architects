using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSettle : MonoBehaviour
{
    private MoveToTarget[] moveToTargets;//场景里所有MoveToTarget的Component
    private bool allSettled = false;
    public GameObject roof;//最后加盖一个屋顶？可以是空Object，仅用来存放动画
    public GameObject explosion;//动画
    private float offset = 5.0f;//动画向上偏移的量
    private float finalClickTimmer = 0.0f;//计时器，点击后过一会儿camera再移动
    private float allSettledTimmer = 0.0f;//计时器，全部到位后过一会儿再爆炸

    [HideInInspector]
    public bool cameraMoveAfterSettled = false;

    private void Start()
    {
        moveToTargets = GameObject.FindObjectsOfType<MoveToTarget>();//找到场景里所有MoveToTarget的Component
    }

    private void Update()
    {
        FinishAndExplode();
    }

    private void FinishAndExplode()
    {
        if (!allSettled)
        {
            int flewNumber = 0;//点击命中数量的计数器
            int settledNumber = 0;//到位数量的计数器

            //计算点击命中（flyingOut）的数量和到位的数量
            for (int i = 0; i < moveToTargets.Length; i++)
            {
                if (moveToTargets[i].flyingOut)
                {
                    flewNumber += 1;
                }
                if (moveToTargets[i].settled)
                {
                    settledNumber += 1;
                }
            }

            //最后一下点击后，延时触发相机移动
            if (flewNumber >= moveToTargets.Length)
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
            if (settledNumber >= moveToTargets.Length)
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
                    for (int i = 0; i < moveToTargets.Length; i++)
                    {
                        moveToTargets[i].gameObject.AddComponent<Rigidbody>();
                        moveToTargets[i].gameObject.GetComponent<Rigidbody>().drag = 5;
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
        }
    }
}