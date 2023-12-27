using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSubtitle : MonoBehaviour
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
        subtitleText.gameObject.SetActive(true);
        StartCoroutine(Typing(subtitleContent, typingSpeed));
        OnQuest?.Invoke();
    }


    public void ShowQuestSubtitle(string content, float speed = 0.07f, float delayTime = 0)
    {
        if (!subtitleText.gameObject.activeSelf)
        {
            hasShow = true;
            subtitleText.gameObject.SetActive(true);
            curCoroutine = StartCoroutine(Typing(content, speed, delayTime));
        }
    }


    IEnumerator Typing(string txt, float speed = 0.07f, float delayTime = 0)
    {
        subtitleText.text = null;

        // 띄어쓰기 두 번이면 줄 바꿈
        if (txt.Contains("  "))
            txt = txt.Replace("  ", "\n");

        yield return new WaitForSeconds(delayTime);

        SoundManager.Instance.PlaySFX("Keyboard");
        for (int i = 0; i < txt.Length; i++)
        {
            subtitleText.text += txt[i];
            yield return new WaitForSeconds(speed);
        }
        SoundManager.Instance.StopSFX();

        yield return new WaitForSeconds(1f);
        subtitleText.gameObject.SetActive(false);
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
