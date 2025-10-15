using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GameManager : MySingleton<GameManager>
{
    //public Transform[] points;      //몬스터 출현위치 저장 배열
    public List<Transform> points = new List<Transform>();      //몬스터 출현위치 저장 List
    public GameObject monster;
    public float createTime;
    bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        /*
        GameObject go = GameObject.Find("SPG");
        if(go!=null)
        {
            Transform spg = go.transform;
            if(spg != null)
            {
                points = spg.GetComponentsInChildren<Transform>();
            }
        }
        */
        Transform spg = GameObject.Find("SPG")?.transform;      //spawnPointGroup 게임오브젝트의 Transform 컴포넌트 추출
        //points = spg?.GetComponentsInChildren<Transform>();     //SpawnPointGroup 하위에 있는 모든 자식 오브젝트의 Transform 컴포넌트 추출
        //spg?.GetComponentsInChildren<Transform>(points);

        foreach (Transform point in spg)
        {
            points.Add(point);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime); //2초 기다렸다가 3초간격으로반복해라
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }

    
}
