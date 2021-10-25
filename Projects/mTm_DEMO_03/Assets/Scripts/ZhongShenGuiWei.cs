using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhongShenGuiWei : MonoBehaviour
{
    public GameObject[] cubes;
    public bool[] wanShirLe;
    bool quanWanShir = false;
    public GameObject roof;
    //public GameObject[] targets;
    //public Dictionary<GameObject, GameObject> GuiWei = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GaiWuDing();
    }
    void GaiWuDing()
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
                for (int i = 0; i < cubes.Length; i++)
                {
                    cubes[i].AddComponent<Rigidbody>();

                }
                roof.SetActive(true);
                Debug.Log("GaiTMD!!");
                quanWanShir = true;
            }
        }
        else
        {
            return;
        }
    }
}
