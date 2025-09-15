using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public AudioClip fireSfx;
    private new AudioSource audio;
    private MeshRenderer muzzleFlash;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>(); //FirePos 하위에 있는 MuzzleFlash의 Material 컴포넌트 추출
        muzzleFlash.enabled = false;    //처음 시작할때는 비활성화
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 1.0f);
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.enabled = true; //MuzzleFlash 활성화

        yield return new WaitForSeconds(0.2f); //0.2초 대기하는 동안 메세지루프로 제어권 양보

        muzzleFlash.enabled = false;    //MuzzleFlash 비활성화
    }
}
