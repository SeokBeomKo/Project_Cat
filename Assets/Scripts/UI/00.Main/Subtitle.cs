using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
 센터

A 발동조건 스크립트 (바이러스 처치)
B 서브타이틀
C 씬 시작 스크립트 (씬 시작)

A 조건완료 > 센터
A 포함되어있는 이벤트 실행
이벤트 > 함수(B 서브타이틀 자막 띄우기 함수)
 
void ClearVirus()
{
    StartCoroutine(subtitle.Typing("바이러스 모두 처치했다 !"))
}

void SceneStart()
{
    StartCoroutine(subtitle.Typing("씬 시작 !"))
}

 */
public class Subtitle : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    
    [Header("표시할 자막 내용")]
    public string subtitleContent;
    [Header("타이핑 속도")]
    public float typingSpeed;

    private bool hasShow = false;

    Coroutine curCoroutine;

    public delegate void questHandle();
    public event questHandle OnQuest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasShow)
        {
            if (!subtitleText.gameObject.activeSelf)
            {
                hasShow = true;
                subtitleText.gameObject.SetActive(true);
                StartCoroutine(Typing(subtitleContent, typingSpeed));
                OnQuest?.Invoke();
            }
            else
            {
                hasShow = true;
                StartCoroutine(WaitForSubtitleDeactivation());
            }
        }
    }

    private IEnumerator WaitForSubtitleDeactivation()
    {
        // 자막이 꺼질 때까지 대기
        while (subtitleText.gameObject.activeSelf)
        {
            yield return null;
        }

        // 자막이 꺼지면 작업 수행
        //hasShow = true;
        subtitleText.gameObject.SetActive(true);
        StartCoroutine(Typing(subtitleContent, typingSpeed));
        OnQuest?.Invoke();
    }

    IEnumerator Typing(string txt, float speed = 0.07f, float delayTime = 0)
    {
        subtitleText.text = null;


        // 띄어쓰기 두 번이면 줄 바꿈
        if (txt.Contains("  "))
            txt = txt.Replace("  ", "\n");
        
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < txt.Length; i++)
        {
            subtitleText.text += txt[i];
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(1f);
        subtitleText.gameObject.SetActive(false);
    }

    public void ShowSubtitle(string content, float speed = 0.07f, float delayTime = 0)
    {
        if (!subtitleText.gameObject.activeSelf)
        {
            hasShow = true;
            subtitleText.gameObject.SetActive(true);
            curCoroutine = StartCoroutine(Typing(content, speed, delayTime));
        }
    }

    public void StopSubtitle()
    {
        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
            subtitleText.gameObject.SetActive(false);
        }
    }
}
