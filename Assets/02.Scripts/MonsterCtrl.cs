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
    private Animator anim;



    //Animator parameter Hash 값 추출    
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    private int hp = 100;


    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;        // 몬스터의 현재상태
    public float traceDist = 10.0f;         // 추적 사정거리
    public float attackDist = 2.0f;         // 공격 사정거리
    public bool isDie = false;              // 몬스터의 사망 여부

    [SerializeField] CapsuleCollider body;
    [SerializeField] SphereCollider[] punch;

    public float TIME_WAIT = 0.3f;

    void OnEnable() // 스크립트가 활성화 될 때
    {
        PlayerCtrl.OnPlayerDie += OnPlayerDie;      //이벤트 발생 시 수행할 함수 연결
    }
    void OnDisable() // 스크립트가 비활성화 될 때
    {
        PlayerCtrl.OnPlayerDie -= OnPlayerDie;      //기존에 연결된 함수 해제
    }


    void Start()
    {
        monsterTr = GetComponent<Transform>();                                      // 몬스터의 Transform 할당

        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();      // 추적대상인 Player 의 Transform 할당

        agent = GetComponent<NavMeshAgent>();                                       // NavMeshAgent 컴포넌트 할당

        //agent.destination = playerTr.position;                                      // 추적대상의 위치를 설정하면 바로 추적시작

        anim = GetComponent<Animator>();

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());

    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(TIME_WAIT);      //0.3초 동안 중지(대기) 하는 동안 제어권을 메세지 루프에 양보

            if (state == State.DIE) yield break; //몬스터의 상태가 DIE 일때 코루틴종료

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

    void OnDrawGizmos()
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
                    anim.SetBool(hashTrace, false);
                    break;

                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;

                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    //몬스터 Collider 비활성화
                    DisableCollider();
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    void DisableCollider()
    {
        //body
        body.enabled = false;
        //punch        
        foreach (var item in punch)
        {
            item.enabled = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            //총알 삭제
            Destroy(collision.gameObject);
            //피격 애니메이션 실행
            anim.SetTrigger(hashHit);

            Vector3 pos = collision.GetContact(0).point;
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);
            // ShowBloodEffect(pos, rot);  << 혈흔효과 생성함수호출

            hp -= 10;
            if (hp <= 0)
            {
                state = State.DIE;
            }


        }
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.2f));
        anim.SetTrigger(hashPlayerDie);

    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    

}
