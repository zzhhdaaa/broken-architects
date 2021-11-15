using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhongShenGuiWei : MonoBehaviour
{
    public GameObject[] cubes;
    public bool[] wanShirLe;
    private bool quanWanShir = false;
    public GameObject roof;
    public GameObject explosion;
    public float offset = 2.0f;
    private float countTime = 0.0f;
    public GameObject Button;

    public GameObject target;

    [HideInInspector]
    public Vector3 targetPos;

    [HideInInspector]
    public bool cameraKuaiPao = false;

    //public GameObject[] targets;
    //public Dictionary<GameObject, GameObject> GuiWei = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        GaiWuDing();
    }

    private void GaiWuDing()
    {
        if (!quanWanShir)
        {
            int wanShirShuLiang = 0;
            for (int i = 0; i < cubes.Length; i++)
            {
                wanShirLe[i] = cubes[i].GetComponent<MoveToTarget>().wanShirLe;
                if (wanShirLe[i])
                {
                    wanShirShuLiang += 1;
                }
            }
            if (wanShirShuLiang >= cubes.Length)
            {
                if (countTime <= 1.8f)
                {
                    countTime += Time.deltaTime;
                    cameraKuaiPao = true;
                }
                else
                {
                    cameraKuaiPao = false;
                    for (int i = 0; i < cubes.Length; i++)
                    {
                        cubes[i].AddComponent<Rigidbody>();
                        cubes[i].GetComponent<Rigidbody>().drag = 5;
                    }
                    Vector3 explosionPos = new Vector3(roof.transform.position.x, roof.transform.position.y - offset, roof.transform.position.z);
                    GameObject obj = Instantiate(explosion, explosionPos, roof.transform.rotation);
                    roof.SetActive(true);
                    Debug.Log("GaiTMD!!");
                    quanWanShir = true;
                }
            }
        }
        else
        {
            return;
        }
    }
}