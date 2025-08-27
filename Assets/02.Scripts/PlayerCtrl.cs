using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]    Transform tr;
    [SerializeField]    float moveSpeed = 10.0f;
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
        //tr.position += Vector3.forward * 1;
        tr.Translate(Vector3.forward * Time.deltaTime*moveSpeed,Space.Self);
    }
}
