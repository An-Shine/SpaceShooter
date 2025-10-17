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
    

    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += OnPlayerDie;
    }
    void Onsable()
    {
        PlayerCtrl.OnPlayerDie -= OnPlayerDie;
    }


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
        Instantiate(bullet, firePos.position, firePos.rotation);        //bullet 프리펩 동적생성
        audio.PlayOneShot(fireSfx, 1.0f);       //총소리 발생
        StartCoroutine(ShowMuzzleFlash());      //총구화염효과 코루틴 함수 호출
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;        //오프셋 좌표값을 랜덤함수로 생성

        muzzleFlash.material.mainTextureOffset = offset;        //텍스쳐의 오프셋 값 설정

        //MuzzleFlash 의 회전 변경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //MuzzleFlash의 크기 조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;


        muzzleFlash.enabled = true; //MuzzleFlash 활성화

        yield return new WaitForSeconds(0.2f); //0.2초 대기하는 동안 메세지루프로 제어권 양보

        muzzleFlash.enabled = false;    //MuzzleFlash 비활성화
    }

    void OnPlayerDie()
    {

    }
}
