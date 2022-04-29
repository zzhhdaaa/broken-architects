using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private MoveToRandomTarget moveToRandomTarget;

    // Start is called before the first frame update
    void Start()
    {
        moveToRandomTarget = GetComponent<MoveToRandomTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToRandomTarget.flyingOut)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), Time.deltaTime * 10);
        }
    }
}
