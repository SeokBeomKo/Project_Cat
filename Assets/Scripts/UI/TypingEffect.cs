using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI endingText;

    public string endingContent;
    private float typingSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        endingText.text = null;

        yield return new WaitForSeconds(typingSpeed);

        for (int i = 0; i < endingContent.Length; i++)
        {
            endingText.text += endingContent[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
