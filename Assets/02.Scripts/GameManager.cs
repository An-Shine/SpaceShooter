using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    //public Transform[] points;      //몬스터 출현위치 저장 배열
    public List<Transform> points = new List<Transform>();      //몬스터 출현위치 저장 List

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

        foreach(Transform point in spg)
        {
            points.Add(point);
        }
    }

    
}
