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

    private string currentText = "";


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            subtitleText.text = "";
            currentText = subtitleContent;
            StartCoroutine(Typing());
            subtitleText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            subtitleText.gameObject.SetActive(false);
        }
    }


    IEnumerator Typing()
    {
        for(int i = 0; i < subtitleContent.Length; i++)
        {
            subtitleText.text += subtitleContent[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
