using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    
    [Header("표시할 자막 내용")]
    public string subtitleContent;
    [Header("타이핑 속도")]
    public float typingSpeed;

    private bool hasShow = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasShow)
        {
            hasShow = true;
            subtitleText.gameObject.SetActive(true);
            StartCoroutine(Typing(subtitleContent));
        }
    }

    IEnumerator Typing(string txt)
    {
        subtitleText.text = null;

        // 띄어쓰기 두 번이면 줄 바꿈
        if (txt.Contains("  "))
            txt = subtitleContent.Replace("  ", "\n");

        for (int i = 0; i < txt.Length; i++)
        {
            subtitleText.text += txt[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        subtitleText.gameObject.SetActive(false);
    }
}
