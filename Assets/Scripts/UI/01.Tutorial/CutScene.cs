using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [Header("�ڸ�")]
    public QuestSubtitle questSubtitle;
    
    [Header("�̵��� �� �̸�")]
    public string SceneName;

    [Header("��ŵ ��ư")]
    public GameObject skipButton;

    [Header("�ε� ��")]
    public LoadScene loadScene;

    private bool isStop = false;

    private void Start()
    {
        if (skipButton != null)
            skipButton.SetActive(true);
        StartCoroutine(CutSceneSubtitle());
    }

    IEnumerator CutSceneSubtitle()
    {
        if (isStop) yield break;

        yield return new WaitForSeconds(11f);
        questSubtitle.ShowQuestSubtitle("ī������ : �� ��ó��, ������ ���Ӻ� Ȯ���߾�~", speed: 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(3.5f);
        questSubtitle.ShowQuestSubtitle("��ó�� : ���� ���� �������� ī����, �� ���п� ��Ҿ�!", speed : 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(3.5f);       
        questSubtitle.ShowQuestSubtitle("ī������ : �������� �� �������� ��¿ ���߾�?  �׷��� �װ� ��ü ����?  �׷��� ���� ��� ó�� �ôµ�.", speed: 0.05f);
        if (isStop) yield break;
        //questSubtitle.ShowQuestSubtitle("ī������ : �׷��� ���� ��� ó�� �ôµ�.");

        yield return new WaitForSeconds(5f); 
        questSubtitle.ShowQuestSubtitle("��ó�� : �������? �� ���� ������!", speed: 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(2.5f);
        questSubtitle.ShowQuestSubtitle("ī������ : �� ���� ����� ĥ���� �ž�.  ���� ���� ���������� �ʹ� ������ ����  �׸� �ξ����� ���ھ�. ���� ��.");
        if (isStop) yield break;
        //questSubtitle.ShowQuestSubtitle("ī������ : �׸� �ξ����� ���ھ�. ���� ��.");
    }

    public void OnClickSkip()
    {
        questSubtitle.StopSubtitle();
        StopCutSceneSubtitle();
        skipButton.SetActive(false);

        loadScene.ChangeSceneCoroutine();
    }

    

    public void StopCutSceneSubtitle ()
    {
        isStop = true;
    }
}
