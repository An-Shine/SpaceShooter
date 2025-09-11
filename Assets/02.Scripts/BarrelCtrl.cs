using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public Texture[] textures;
    public float radius = 10.0f;
    new MeshRenderer renderer;
    Transform tr;
    Rigidbody rb;
    int hitCount = 0;
    Collider[] colls = new Collider[10];

    const int HIT_COUNT = 3;
    const float DESTROY_EXP = 5.0f;
    const float DESTROY_BARREL = 3.0f;
    const float BARREL_MASS = 1.0f;
    const float UP_FORCE = 1500.0f;
    





    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);     // 난수 발생

        renderer.material.mainTexture = textures[idx];  //텍스쳐 지정


    }

    void OnCollisionEnter(Collision collision)
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
        //rb.mass = BARREL_MASS;
        //rb.AddForce(Vector3.up * UP_FORCE);
        IndirectDamage(tr.position);

        Destroy(gameObject, DESTROY_BARREL);


    }

    void IndirectDamage(Vector3 pos)
    {
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);
        foreach (var item in colls)
        {
            if (item == null) continue;
            rb = item.GetComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }


}
