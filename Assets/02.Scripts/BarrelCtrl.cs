using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    Transform tr;
    Rigidbody rb;
    int hitCount = 0;

    const int HIT_COUNT = 3;
    const float DESTROY_EXP = 5.0f;
    const float DESTROY_BARREL = 3.0f;
    const float BARREL_MASS = 1.0f;
    const float UP_FORCE = 1500.0f;




    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void OCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (++hitCount == HIT_COUNT)
            {
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, DESTROY_EXP);
        rb.mass = BARREL_MASS;
        rb.AddForce(Vector3.up * UP_FORCE);
        Destroy(gameObject, DESTROY_BARREL);


    }
}
