using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    delegate float SumHandler(float a, float b);
    // delegate 타입의 변수 선ㅅ언
    SumHandler sumHandler;

    float Sum(float a, float b)
    {
        return a + b;
    }
    void Start()
    {
        //delegate 변수에 메서드 연결 할당
        sumHandler = Sum;
        //delegate 실행
        float sum = sumHandler(10.0f, 5.0f);
        //결과값 출력
        Debug.Log($"Sum = {sum}");
    }
}
