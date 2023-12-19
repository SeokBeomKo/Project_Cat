using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [Header("자막")]
    public QuestSubtitle questSubtitle;
    
    [Header("이동할 씬 이름")]
    public string SceneName;

    [Header("스킵 버튼")]
    public GameObject skipButton;

    [Header("로드 씬")]
    public LoadScene loadScene;

    private bool isStop = false;

    private void Start()
    {
        skipButton.SetActive(true);
        StartCoroutine(CutSceneSubtitle());
    }

    IEnumerator CutSceneSubtitle()
    {
        if (isStop) yield break;

        yield return new WaitForSeconds(11f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 어 리처드, 보내준 공임비 확인했어~", speed: 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(3.5f);
        questSubtitle.ShowQuestSubtitle("리처드 : 오늘 정말 고마웠어 카나리, 너 덕분에 살았어!", speed : 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(3.5f);       
        questSubtitle.ShowQuestSubtitle("카날리아 : 정말이지 나 없었으면 어쩔 뻔했어?  그런데 그거 대체 뭐야?  그렇게 작은 쥐는 처음 봤는데.", speed: 0.05f);
        if (isStop) yield break;
        //questSubtitle.ShowQuestSubtitle("카날리아 : 그렇게 작은 쥐는 처음 봤는데.");

        yield return new WaitForSeconds(5f); 
        questSubtitle.ShowQuestSubtitle("리처드 : 대박이지? 내 연구 성과야!", speed: 0.06f);
        if (isStop) yield break;

        yield return new WaitForSeconds(2.5f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 또 무슨 사고를 칠려는 거야.  물론 나야 응원하지만 너무 무모한 짓은  그만 두었으면 좋겠어. 내일 봐.");
        if (isStop) yield break;
        //questSubtitle.ShowQuestSubtitle("카날리아 : 그만 두었으면 좋겠어. 내일 봐.");
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