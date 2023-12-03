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


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !hasShow)
        {
            if (!subtitleText.gameObject.activeSelf)
            {
                hasShow = true;
                subtitleText.gameObject.SetActive(true);
                StartCoroutine(Typing(subtitleContent));
            }
        }
    }

    IEnumerator Typing(string txt)
    {
        subtitleText.text = null;

        // 띄어쓰기 두 번이면 줄 바꿈
        if (txt.Contains("  "))
            txt = txt.Replace("  ", "\n");

        for (int i = 0; i < txt.Length; i++)
        {
            subtitleText.text += txt[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1f);
        subtitleText.gameObject.SetActive(false);
    }

    public void ShowSubtitle(string content)
    {
        if (!subtitleText.gameObject.activeSelf)
        {
            hasShow = true;
            subtitleText.gameObject.SetActive(true);
            StartCoroutine(Typing(content));
        }
    }
}
