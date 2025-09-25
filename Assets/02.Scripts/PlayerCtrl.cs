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
    public float currHp;
    const float PUNCH_POWER = 10.0f;
    const float TIME_INTER = 0.25f;
    const float INPUT_VALUE = 0.1f;
    const float INIT_HP = 100.0f;

    //delegate 선언
    public delegate void PlayerDieHandler();

    //event 선언, 아무때나 불러쓸수있도록 static
    public static event PlayerDieHandler OnPlayerDie;


    IEnumerator Start()
    {
        currHp = INIT_HP;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");
        //Debug.Log($"h={h}");
        //Debug.Log($"v={v}");
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
    void OnTriggerEnter(Collider coll)
    {
        if (currHp > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= PUNCH_POWER;
            Debug.Log($"Player HP={currHp / INIT_HP}");
            //Debug.LogFormat("Player HP = {0}", currHp / INIT_HP);
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die!");
        /*
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (var item in monsters)
        {
            //monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
            item.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
        */
        OnPlayerDie();
        
    }
    
    
}
