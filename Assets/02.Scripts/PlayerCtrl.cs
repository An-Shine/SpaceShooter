using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Transform tr;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float turnSpeed = 80.0f;
    Animation anim;
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");
        Debug.Log($"h={h}");
        Debug.Log($"v={v}");
        //transform.position += new Vector3(0, 0, 1);

        //transform.position += Vector3.forward * 1;
        //tr.position += Vector3.forward * 1;
        //tr.Translate(Vector3.forward * Time.deltaTime*moveSpeed,Space.Self);
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
        PlayerAnim(h, v);

    }
    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h < -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }

        
        

        
    }
}
