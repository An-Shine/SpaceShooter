using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public AudioClip fireSfx;
    private new AudioSource audio;


    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 1.0f);
    }
}

