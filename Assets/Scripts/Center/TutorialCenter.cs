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

    [Header("UI 인풋 핸들")]
    [SerializeField] public UIInputHandler uiInputHandler;


    [Header("고양이")]
    [SerializeField] public GameObject cat;

    [Header("고양이 충돌 감지")]
    [SerializeField] public ExitCatMove exitCatMove;

    [Header("플레이어 자막 충돌 감지")]
    [SerializeField] public PlayerSubtitle clearPlayerSubtitle;

    [Header("플레이어 가이드 충돌 감지")]
    [SerializeField] public PlayerGuide keyGuide;
    [SerializeField] public PlayerGuide itemGuide;
    [SerializeField] public PlayerGuide gunGuide;
    [SerializeField] public PlayerGuide virusGuide;

    [Header("게임 가이드")]
    [SerializeField] public GameGuide gameGuide;


    [Header("자막")]
    [SerializeField] public Subtitle subtitle;
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("바이러스 그룹")]
    [SerializeField] public GameObject VirusGroup;

    [Header("UI 컨트롤러")]
    [SerializeField] public UIController controllerUI;

    [Header("클리어 포인트")]
    [SerializeField] public GameObject endPoint;

    [Header("커서 이벤트")]
    public CursorEvent cursor;

    [Header("스킵 버튼")]
    public GameObject skipButton;

    [Header("전화 화면")]
    public GameObject voiceMessage;

    [Header("퀘스트 UI")]
    public QuestPopUp questUI;

    [Header("아이템 휠")]
    public GameObject itemWheel;

    private bool isStop = false;

    private void Start() 
    {
        PlayerPrefs.SetString("nextScene", "04.Tutorial");

        stopWatch.OnSubtitle += OnCellPhone; // 전화 컷씬
        exitCatMove.OnExitCat += ExitCatMove; // 고양이 컷씬

        keyGuide.OnPlayerGuide += OnKeyGuide;
        itemGuide.OnPlayerGuide += OnItemGuide;
        gunGuide.OnPlayerGuide += OnGunGuide;
        virusGuide.OnPlayerGuide += OnVirusGuide;

        clearPlayerSubtitle.OnPlayerSubtitle += OnClearGuide; // 튜토리얼 클리어

        gameGuide.OnCloseKey += CloseKey;
        gameGuide.OnCloseItem += CloseItem;
        gameGuide.OnCloseGun += CloseGun;
        gameGuide.OnCloseVirus += CloseVirus;

        VirusGroup.SetActive(false);
        controllerUI.RemoveUI();
        cameraRotate.SetActive(false);
        inputHandler.gameObject.SetActive(false);   

        questUI.DeactivatePopUp();
    }

    public void OnCellPhone()
    {
        itemWheel.SetActive(false);
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

        controllerUI.RemoveUI();
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
    public void OnClickSkip()
    {
        itemWheel.SetActive(true);
        questSubtitle.StopSubtitle();
        subtitle.StopSubtitle();
        StopPhoneSubtitle();
        skipButton.SetActive(false);

        Invoke("ExitCatMove", 0.2f);
    }

    public void ExitCatMove()
    {
        cameraController.SetPlayCamera();
        controllerUI.ShowUI();
        questSubtitle.ShowQuestSubtitle("애완 고양이 로키를 쫓아가자", delayTime : 0.5f);      
        cursor.CursorOff();        

        voiceMessage.SetActive(false);
        cat.SetActive(false);
        inputHandler.gameObject.SetActive(true);
        cameraRotate.SetActive(true);
        VirusGroup.SetActive(true);
        skipButton.SetActive(false);
        itemWheel.SetActive(true);
    }

    public void OnKeyGuide()
    {
        gameGuide.ShowKeyPopUp();
        cursor.CursorOn();
        inputHandler.gameObject.SetActive(false);
        uiInputHandler.gameObject.SetActive(false);
        cameraRotate.SetActive(false);
        controllerUI.RemoveUI();
        itemWheel.SetActive(false);
    }


    public void CloseKey()
    {
        cursor.CursorOff();
        inputHandler.gameObject.SetActive(true);
        uiInputHandler.gameObject.SetActive(true);
        cameraRotate.SetActive(true);
        controllerUI.ShowUI();
        questUI.ActivatePopUP("조작키 학습", "앞으로 이동하세요.");
        itemWheel.SetActive(true);
    }

    public void OnItemGuide()
    {
        questUI.DeactivatePopUp();
        gameGuide.ShowItemPopUp();
        cursor.CursorOn();
        inputHandler.gameObject.SetActive(false);
        uiInputHandler.gameObject.SetActive(false);
        cameraRotate.SetActive(false);
        controllerUI.RemoveUI();
        itemWheel.SetActive(false);
    }
    public void CloseItem()
    {
        cursor.CursorOff();
        inputHandler.gameObject.SetActive(true);
        uiInputHandler.gameObject.SetActive(true);
        cameraRotate.SetActive(true);
        controllerUI.ShowUI();
        questUI.ActivatePopUP("아이템 획득", "아이템을 획득해보세요.");
        itemWheel.SetActive(true);
    }

    public void OnGunGuide()
    {
        questUI.DeactivatePopUp();
        gameGuide.ShowGunPopUp();
        cursor.CursorOn();
        inputHandler.gameObject.SetActive(false);
        uiInputHandler.gameObject.SetActive(false);
        cameraRotate.SetActive(false);
        controllerUI.RemoveUI();
        itemWheel.SetActive(false);
    }

    public void CloseGun()
    {
        cursor.CursorOff();
        inputHandler.gameObject.SetActive(true);
        uiInputHandler.gameObject.SetActive(true);
        cameraRotate.SetActive(true);
        controllerUI.ShowUI();
        questSubtitle.ShowQuestSubtitle("캡슐 안에 뭐가 들어있는지 확인해보자!");
        questUI.ActivatePopUP("캡슐을 뿌셔뿌셔", "총을 이용해 캡슐 안 아이템을  획득해 보세요.");
        itemWheel.SetActive(true);
    }

    public void OnVirusGuide()
    {
        questUI.DeactivatePopUp();
        gameGuide.ShowVirusPopUp();
        cursor.CursorOn();
        inputHandler.gameObject.SetActive(false);
        uiInputHandler.gameObject.SetActive(false);
        cameraRotate.SetActive(false);
        controllerUI.RemoveUI();
        itemWheel.SetActive(false);
    }

    public void CloseVirus()
    {
        cursor.CursorOff();
        inputHandler.gameObject.SetActive(true);
        uiInputHandler.gameObject.SetActive(true);
        cameraRotate.SetActive(true);
        controllerUI.ShowUI();
        subtitle.ShowSubtitle("카날리아 : 탁자 위에 공을 활용할 수 있을거 같아"); 
        questUI.ActivatePopUP("공굴리기 활용", "물총으로 물총으로 공을  아래로 떨어뜨려보자");
        itemWheel.SetActive(true);
    }


    public void OnClearGuide()
    {
        controllerUI.RemoveUI();
        cameraRotate.SetActive(false);
        itemWheel.SetActive(false);
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

    /*
    이 효과적인 용액은 바이러스를 사라지게 만들지만, 인체에 들어와 변형된 바이러스는 없애지 못해.
    우리가 최대한 빨리 연구해서 돌아갈 방법을 찾아볼게
    */

}