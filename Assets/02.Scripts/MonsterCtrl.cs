using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    // 컴포넌트의 캐시를 처리할 변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;        // 몬스터의 현재상태
    public float traceDist = 10.0f;         // 추적 사정거리
    public float attackDist = 2.0f;         // 공격 사정거리
    public bool isDie = false;              // 몬스터의 사망 여부

    void Start()
    {
        monsterTr = GetComponent<Transform>();                                      // 몬스터의 Transform 할당

        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();      // 추적대상인 Player 의 Transform 할당

        agent = GetComponent<NavMeshAgent>();                                       // NavMeshAgent 컴포넌트 할당

        //agent.destination = playerTr.position;                                      // 추적대상의 위치를 설정하면 바로 추적시작

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());

    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);      //0.3초 동안 중지(대기) 하는 동안 제어권을 메세지 루프에 양보

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);       //몬스터와 주인공 캐릭터 사이의 거리 측정

            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }            
        }
    }

    void ODrawGizmos()
    {
        if (state == State.TRACE)       //추적 사정거리 표시
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }

        if (state == State.ATTACK)       //공격 사정거리 표시
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    break;

                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    break;

                case State.ATTACK:
                    break;

                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    void Update()
    {
        
    }
}
