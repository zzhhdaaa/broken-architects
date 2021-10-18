using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiYiShi : MonoBehaviour
{
    public GameObject lianjie;
    int v = 89;
    int n = 32;
    int y = 13;

    // Start is called before the first frame update
    void Start()
    {
        lianjie.GetComponent<MouseManager>().AddAge(v, n);
        lianjie.GetComponent<MouseManager>().AddAndPrint(v, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
