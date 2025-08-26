using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log($"h={h}");
        Debug.Log($"v={v}");
        //transform.position += new Vector3(0, 0, 1);

        //transform.position += Vector3.forward * 1;
        tr.position += Vector3.forward * 1;
    }
}
