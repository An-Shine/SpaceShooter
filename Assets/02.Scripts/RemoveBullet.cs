using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Bullet")
        //if(collision.gameObject.tag == "Bulet")
        if(collision.collider.CompareTag("Bullet"))
        {
            ContactPoint cp = collision.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            //Instantiate(sparkEffect, cp.point, rot);
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            Destroy(spark, 0.5f);

            Destroy(collision.gameObject);
        }
    }
}
