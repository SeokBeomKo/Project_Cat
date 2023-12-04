using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StopWatch : MonoBehaviour
{
    public delegate void StopWatchHandle();
    public event StopWatchHandle OnSubtitle;

    public TextMeshProUGUI timerText;
    private float callTime = 0;

    private void OnEnable() 
    {
        StartCoroutine(TimerCouroutine());
        OnSubtitle?.Invoke();
    }

    IEnumerator TimerCouroutine()
    {
        callTime += 1;

        timerText.text = Mathf.Floor(callTime / 3600).ToString("00") + ":" + Mathf.Floor(callTime % 60).ToString("00");

        yield return new WaitForSeconds(1f);

        StartCoroutine(TimerCouroutine());
    }
}

