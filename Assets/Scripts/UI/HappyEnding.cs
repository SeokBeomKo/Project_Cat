using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HappyEnding : MonoBehaviour
{
    public QuestSubtitle questSubtitle;
    public Subtitle subtitle;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("HappyEnding");
        StartCoroutine(ShowSubtitle());
    }

    public IEnumerator ShowSubtitle()
    {
        yield return new WaitForSeconds(1f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 로키...야..도와...줘...");
        yield return new WaitForSeconds(8f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 콜록! 콜록! 물을... 너무 많이 먹었어...");
        yield return new WaitForSeconds(20f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 그래도 씻으니까 개운하지?");
        yield return new WaitForSeconds(5f);
        questSubtitle.ShowQuestSubtitle("로키 : 애옹");
        yield return new WaitForSeconds(2f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 그래 그래, 살려줘서 고마워.  역시 로키라니까.");
        yield return new WaitForSeconds(5f);
        questSubtitle.ShowQuestSubtitle("카날리아 : 널 만나서 정말 다행이야.  앞으로도 잘 부탁해!");
        yield return new WaitForSeconds(5f);
        questSubtitle.ShowQuestSubtitle("로키 : 애옹");
    }
}
