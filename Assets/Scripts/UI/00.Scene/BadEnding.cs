using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BadEnding : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public GameObject button;

    public Image topImage;
    public Image bottomImage;
    
    [Header("재시작할 씬 이름")]
    public string researchScene;
    public string runningScene;
    public string washingScene;

    [Header("로비씬")]
    public string lobbySceneName;

    [Header("자막")]
    public QuestSubtitle questSubtitle;

    private float progress = 0f;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("BadEnding");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(Ending());
        button.SetActive(false);
    }

    IEnumerator Ending()
    {
        questSubtitle.ShowQuestSubtitle("그냥 오늘은 쉴까 ... ?", delayTime: 0.5f);

        yield return new WaitForSeconds(2.5f);
        endingText.gameObject.SetActive(false);

        while(progress < 1f)
        {
            progress += Time.deltaTime * 1.3f;
            topImage.fillAmount = Mathf.Lerp(1f, 0f, progress);
            bottomImage.fillAmount = Mathf.Lerp(1f, 0f, progress);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        button.SetActive(true);
    }

    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySFX("Click");
        if (PlayerPrefs.GetInt("Restart") == 1)
            SceneManager.LoadScene(researchScene);
        if(PlayerPrefs.GetInt("Restart") == 2)
            SceneManager.LoadScene(runningScene);
        if (PlayerPrefs.GetInt("Restart") == 3)
            SceneManager.LoadScene(washingScene);
    }

    public void OnClickEnd()
    {
        SoundManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene(lobbySceneName);
    }

}
