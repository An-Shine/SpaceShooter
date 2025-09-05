using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Bullet")
        //if(collision.gameObject.tag == "Bulet")
        if(collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
