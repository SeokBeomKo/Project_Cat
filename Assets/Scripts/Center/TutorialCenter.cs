using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCenter : MonoBehaviour
{
    [Header("카메라 컨트롤러")]
    [SerializeField] public TutorialCameraController cameraController;
    [Header("음성 전화 애니메이터")]
    [SerializeField] public Animator voiceMessageAnimator;

    [Header("핸드폰 타이머")]
    [SerializeField] public StopWatch stopWatch;

    [Header("인풋 핸들")]
    [SerializeField] public InputHandler inputHandler;

    [Header("고양이")]
    [SerializeField] public GameObject cat;

    [Header("고양이 충돌 감지")]
    [SerializeField] public ExitCatMove exitCatMove;

    [Header("자막")]
    [SerializeField] public Subtitle subtitle;
    [SerializeField] public QuestSubtitle questSubtitle;

    private void Start() 
    {
        stopWatch.OnSubtitle += OnCellPhone;
        exitCatMove.OnExitCat += ExitCatMove;
    }

    public void ExitCatMove()
    {
        cameraController.SetPlayCamera();
        cat.SetActive(false);
        inputHandler.gameObject.SetActive(true);

        questSubtitle.ShowQuestSubtitle("애완 고양이 로키를 쫓아가자");
    }

    public void OnCellPhone()
    {
        inputHandler.gameObject.SetActive(false);
        StartCoroutine(PhoneSubtitle());
    }

    public IEnumerator PhoneSubtitle()
    {
        subtitle.ShowSubtitle("리처드 : 카날리아, 괜찮아 ?  너가 실험 기계를 고쳐줬지만,  우리쪽의 실수로 인해 다들 바이러스 X에 감염되어 몸이 작아졌어.");
        yield return new WaitForSeconds(10f);

        subtitle.ShowSubtitle("리처드 : 하지만 치료법은 이미 찾아서 지금 알려줄게.  물과 고양이 샴푸를 50:1 비율로 섞어.");

        yield return new WaitForSeconds(7f);

        subtitle.ShowSubtitle("리처드 : 왜 고양이 샴푸냐고?  전파 매체가 바로 고양이 털이기 때문이야");

        yield return new WaitForSeconds(6f);

        subtitle.ShowSubtitle("리차드 : 이 효과적인 용액은 바이러스를 사라지게 만들지만,  인체에 들어와 변형된 바이러스는 없애지 못해.");

        yield return new WaitForSeconds(8f);

        subtitle.ShowSubtitle("리처드 : 우리가 최대한 빨리 연구해서 돌아갈 방법을 찾아볼게.  행운을 빌어 카날리아.");

        yield return new WaitForSeconds(7f);

        voiceMessageAnimator.SetBool("isHide",true);

        yield return new WaitForSeconds(2f);

        subtitle.ShowSubtitle("카날리아 : 고양이 털이 매개라면, 로키를 찾아야.. 로키잖아?!");

        yield return new WaitForSeconds(4f);
        EnterCatMove();
    }

    public void EnterCatMove()
    {
        cameraController.SetCatCamera();
        cat.SetActive(true);
    }

/*
이 효과적인 용액은 바이러스를 사라지게 만들지만, 인체에 들어와 변형된 바이러스는 없애지 못해.
우리가 최대한 빨리 연구해서 돌아갈 방법을 찾아볼게
*/

}