using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Image progressBar;
    // private string sceneName;

    private float loadingTime = 5.0f;
    private float time;

    void Start()
    {
        //progressBar = GetComponent<Image>();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        // �񵿱� �ε� : Scene�� �ҷ��� �� ������ �ʰ� �ٸ� �۾� ����
        AsyncOperation operation = SceneManager.LoadSceneAsync("HanKyeol_HUD"); // �񵿱������� �ε� ����
        operation.allowSceneActivation = false; // �� �ε� �� �ڵ����� ��� ��ȯ�� ���� �ʵ���

        while (!operation.isDone) // �ε� �Ϸ� ���� 
        {
            yield return null;

            time += Time.deltaTime;
            progressBar.fillAmount = Mathf.Clamp01(time / loadingTime);

            if (time > loadingTime)
                operation.allowSceneActivation = true;
        }

    }

}

// operation.progress : ���������� float�� 0,1�� ��ȯ (0 : ���� �� , 1 : ����Ϸ�)
// Mathf.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
