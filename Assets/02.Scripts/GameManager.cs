using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEditor;

public class GameManager : MySingleton<GameManager>
{
    //public Transform[] points;      //몬스터 출현위치 저장 배열
    public List<Transform> points = new List<Transform>();      //몬스터 출현위치 저장 List
    public List<GameObject> monsterPool = new List<GameObject>();   //몬스터를 미리 생성해 저장할 리스트 자료형
    public int maxMonsters = 10;
    public GameObject monster;
    public float createTime = 3.0f;
    public TMP_Text scoreText;
    int totScore = 0;
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
        CreateMonsterPool();
        // 몬스터 오브젝트 풀 생성

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

        //점수 보여줌
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);  //key 값을 가져오겠다
        DisplayScore(0);
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        //Instantiate(monster, points[idx].position, points[idx].rotation);
        //오브젝트 풀에서 몬스터 추출
        GameObject _monster = GetMonsterInPool();
        //추출한 몬스터의 위치와 회전값 설정
        _monster?.transform.SetLocalPositionAndRotation(points[idx].position, points[idx].rotation);
        //추출한 몬스터를 활성화
        _monster?.SetActive(true);
    }
    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            var _monster = Instantiate<GameObject>(monster);    //몬스터 생성
            _monster.name = $"Monster_{i:00}";                  //몬스터 이름을 지정
            _monster.SetActive(false);                          //몬스터 비활성화
            monsterPool.Add(_monster);                    //생성한 몬스터를 오브젝트 풀에 추가
        }
    }

    public GameObject GetMonsterInPool()
    {
        foreach (var _monster in monsterPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;
            }
        }
        return null;
    }
    /// <summary>
    /// 점수를 누적하고 출력하는 함수
    /// </summary>
    /// <param name="score"></param>
    public void DisplayScore(int score)
    {
        totScore += score;
        scoreText.text = $"<color=#00ff00>Score : </color><color=#ff0000>{totScore:#,##0}</color>";
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

    [MenuItem("AKH/Reset Score")]
    public static void ResetScore()
    {
        PlayerPrefs.SetInt("TOT_SCORE", 0);
        Debug.Log("Reset Score....");
    }

    
}
