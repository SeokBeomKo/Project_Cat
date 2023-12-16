using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCenter : MonoBehaviour
{
    [Header("카메라 로테이트")]
    [SerializeField] public GameObject cameraRotate;

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

    [Header("플레이어 충돌 감지")]
    [SerializeField] public PlayerSubtitle ballPlayerSubtitle;
    [SerializeField] public PlayerSubtitle clearPlayerSubtitle;

    [Header("자막")]
    [SerializeField] public Subtitle subtitle;
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("바이러스 그룹")]
    [SerializeField] public GameObject VirusGroup;

    [Header("캔버스 그룹")]
    [SerializeField] public GameObject UIGroup;

    [Header("클리어 포인트")]
    [SerializeField] public GameObject endPoint;

    [Header("커서 이벤트")]
    public GameObject cursor;

    [Header("스킵 버튼")]
    public GameObject skipButton;

    [Header("전화 화면")]
    public GameObject voiceMessage;

    [Header("퀘스트 UI")]
    public QuestPopUp questUI;

    private bool isStop = false;

    private void Start() 
    {
        stopWatch.OnSubtitle += OnCellPhone;
        exitCatMove.OnExitCat += ExitCatMove;

        ballPlayerSubtitle.OnPlayerSubtitle += OnBallGuide;
        clearPlayerSubtitle.OnPlayerSubtitle += OnClearGuide;

        VirusGroup.SetActive(false);
        UIGroup.SetActive(false);
        cameraRotate.SetActive(false);
        inputHandler.gameObject.SetActive(false);
        cursor.SetActive(false);

        questUI.DeactivatePopUp();
    }

    public void OnCellPhone()
    {
        inputHandler.gameObject.SetActive(false);
        skipButton.SetActive(true);
        StartCoroutine(PhoneSubtitle());
    }

    public void StopPhoneSubtitle()
    {
        isStop = true;
    }

    public IEnumerator PhoneSubtitle()
    {
        questSubtitle.ShowQuestSubtitle("리처드 : 카날리아, 괜찮아 ?  너가 실험 기계를 고쳐줬지만,  우리쪽의 실수로 인해 다들 바이러스 X에 감염되어 몸이 작아졌어.");

        yield return new WaitForSeconds(7f);
        if(isStop) yield break;

        questSubtitle.ShowQuestSubtitle("리처드 : 하지만 치료법은 이미 찾아서 지금 알려줄게.  물과 고양이 샴푸를 50:1 비율로 섞어.");

        yield return new WaitForSeconds(6.5f);
        if(isStop) yield break;

        questSubtitle.ShowQuestSubtitle("리처드 : 왜 고양이 샴푸냐고?  전파 매체가 바로 고양이 털이기 때문이야");

        yield return new WaitForSeconds(6f);
        if(isStop) yield break;

        questSubtitle.ShowQuestSubtitle("리처드 : 이 효과적인 용액은 바이러스를 사라지게 만들지만,  인체에 들어와 변형된 바이러스는 없애지 못해.");

        yield return new WaitForSeconds(7f);
        if (isStop) yield break;

        questSubtitle.ShowQuestSubtitle("리처드 : 우리가 최대한 빨리 연구해서 돌아갈 방법을 찾아볼게.  행운을 빌어 카날리아.");

        yield return new WaitForSeconds(7f);
        if (isStop) yield break;

        UIGroup.SetActive(false);
        cameraRotate.SetActive(false);
        inputHandler.gameObject.SetActive(false);
        VirusGroup.SetActive(false);

        voiceMessageAnimator.SetBool("isHide",true);

        yield return new WaitForSeconds(1f);
        
        EnterCatMove();
    }
    public void EnterCatMove()
    {
        cameraController.SetCatCamera();
        subtitle.ShowSubtitle("카날리아 : 고양이 털이 매개라면, 로키를 찾아야.. 로키잖아?!", delayTime : 1.3f);
        cat.SetActive(true);
    }
    public void ExitCatMove()
    {
        cameraController.SetPlayCamera();
        cat.SetActive(false);
        inputHandler.gameObject.SetActive(true);
        UIGroup.SetActive(true);
        cameraRotate.SetActive(true);
        VirusGroup.SetActive(true);
        questSubtitle.ShowQuestSubtitle("애완 고양이 로키를 쫓아가자", delayTime : 0.5f);
    }

    public void OnBallGuide()
    {
        StartCoroutine(BallGuideSubtitle());
    }

    IEnumerator BallGuideSubtitle()
    {
        subtitle.ShowSubtitle("카날리아 : 탁자 위에 공을 활용할 수 있을거 같아");

        yield return new WaitForSeconds(3f);

        //questSubtitle.ShowQuestSubtitle("물총으로 공을 아래로 떨어뜨려보자");*/
        questUI.ActivatePopUP("공굴리기 활용", "물총으로 물총으로 공을 아래로 떨어뜨려보자");
    }


    public void OnClearGuide()
    {
        UIGroup.SetActive(false);
        cameraRotate.SetActive(false);
        inputHandler.gameObject.SetActive(false);
        questUI.DeactivatePopUp();
        subtitle.ShowSubtitle("카날리아 : 바이러스가 퍼지는 것을 막기 위해,  나가서 컴퓨터로 모든 문을 잠그자!");
        questUI.ActivatePopUP("이동하기", "다음 맵으로 이동하십시오");
        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(4f);

        endPoint.SetActive(true);
    }

    public void OnClickSkip()
    {
        questSubtitle.StopSubtitle();
        subtitle.StopSubtitle();

        StopPhoneSubtitle();
        voiceMessage.SetActive(false);
        cursor.SetActive(true);
        skipButton.SetActive(false);

        Invoke("ExitCatMove", 0.2f);
    }

/*
이 효과적인 용액은 바이러스를 사라지게 만들지만, 인체에 들어와 변형된 바이러스는 없애지 못해.
우리가 최대한 빨리 연구해서 돌아갈 방법을 찾아볼게
*/

}