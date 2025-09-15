using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    
    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.CompareTag("BULLET"))
        {          
            ContactPoint cp = collision.GetContact(0);                  //첫번째 충돌지점의 정보 추출
            Quaternion rot = Quaternion.LookRotation(-cp.normal);       //충돌한 총알의 법선벡터를 쿼터니언타입으로 변환
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);     //스파크 파티클을 동적으로 생성

            Destroy(spark, 0.5f);

            Destroy(collision.gameObject);
        }
    }
}
