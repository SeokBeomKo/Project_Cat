using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public GameObject button;

    public Image topImage;
    public Image bottomImage;
    
    [Header("재시작 시 이동할 씬 이름")]
    public string sceneName;

    private string endingContent = "그냥 오늘은 쉴까 ... ?";

    private float progress = 0f;

    private void Start()
    {
        StartCoroutine(Typing());
        button.SetActive(false);
    }

    IEnumerator Typing()
    {
        endingText.text = null;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < endingContent.Length; i++)
        {
            endingText.text += endingContent[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        endingText.gameObject.SetActive(false);

        while(progress < 1f)
        {
            progress += Time.deltaTime * 1.3f;
            topImage.fillAmount = Mathf.Lerp(1f, 0f, progress);
            bottomImage.fillAmount = Mathf.Lerp(1f, 0f, progress);
            yield return null;
        }

        // 자막이 끝난 후 버튼 활성화
        yield return new WaitForSeconds(0.2f);
        button.SetActive(true);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickEnd()
    {
        SceneManager.LoadScene("HanKyeol_Lobby");
    }

}
