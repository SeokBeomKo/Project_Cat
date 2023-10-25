using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    [SerializeField]
    private float limitTime = 36000; // 제한 시간 10분

    private void Start()
    {
        /*StartCoroutine(TimerCouroutine());*/
    }

    private void Update()
    {
        limitTime -= Time.deltaTime * 60;

        timerText.text = Mathf.Floor(limitTime / 3600).ToString("00") + ":" + Mathf.Floor((limitTime / 60) % 60).ToString("00");

        if (limitTime == 0)
            Debug.Log("게임 종료");

        //yield return null;
        //StartCoroutine(TimerCouroutine());
    }
}

/*
if (gameTime == 0)
    Debug.Log("게임 종료");

ToString(D2) : 2일 때 02로 표시
*/