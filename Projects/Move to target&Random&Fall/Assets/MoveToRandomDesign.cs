using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRandomDesign : MonoBehaviour
{
    public Transform target;

    private Element[] elements;

    public int count;
    public float gap;

    public Material white;
    public Material red;

    // Start is called before the first frame update
    private void Start()
    {
        elements = GameObject.FindObjectsOfType<Element>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveToRandom();
        SettleDown();
    }

    private void MoveToRandom()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomNum = Random.Range(0, elements.Length);
            elements[randomNum].gameObject.transform.position = target.position + new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), gap * count, UnityEngine.Random.Range(-10.0f, 10.0f));
            Destroy(elements[randomNum].gameObject.GetComponent<Rigidbody>());
            count += 1;
        }
    }

    private void SettleDown()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].gameObject.GetComponent<Rigidbody>() == null)
                {
                    elements[i].gameObject.AddComponent<Rigidbody>();
                    elements[i].gameObject.GetComponent<MeshRenderer>().material = white;
                }
                else
                {
                    elements[i].gameObject.GetComponent<MeshRenderer>().material = red;
                }
            }
        }
    }
}