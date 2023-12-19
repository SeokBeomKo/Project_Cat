using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenter : MonoBehaviour
{                                                                   
    [Header("�÷��̾� �ڸ�")]
    [SerializeField] public Subtitle subtitle;

    [Header("����Ʈ �ڸ�")]
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("UI")]
    [SerializeField] public UIController controllerUI;

    [Header("CCTV ����Ʈ")]
    [SerializeField] public CCTV cctv;

    [Header("�� ������Ʈ")]
    [SerializeField] public Barrier barrier;

    [Header("�� ������Ʈ")]
    [SerializeField] public Door door;

    [Header("����ġ ������Ʈ")]
    [SerializeField] public SwitchesOperation switches;

    [Header("ī�޶� ����")]
    [SerializeField] public ResearchCameraController cameraController;
    
    [Header("�⺻ �Է� ����")]
    [SerializeField] public InputHandler inputHandler;

    [Header("���� ����")]
    public GameObject endPoint;

    [Header("����Ʈ UI")]
    public QuestPopUp questUI;

    [Header("����Ʈ ��ġ")]
    public QuestSubtitle firstQuest; // ���̷��� ����
    public QuestSubtitle secondQuest; // ���� �ö���
    public QuestSubtitle thirdQuest;

    [Header("������ ��")]
    public GameObject itemWheel;

    [Header("������")]
    [SerializeField] public GameObject cat;

    private void Start()
    {
        PlayerPrefs.SetString("nextScene", "05.Research");

        StartCoroutine(StartResearchScene());
        endPoint.SetActive(false);
        questUI.DeactivatePopUp();

        firstQuest.OnQuest += RemovalVirus;
        secondQuest.OnQuest += GoUp;
        thirdQuest.OnQuest += UpTable;

        switches.InitSwitch(false);
        cctv.OnCCTV += ShutBarrier; // CCTV �ѱ�
        barrier.OnBarrier += ShutDoor; // �庮 ��������
        door.OnCloseDoor += OnSwitch; // �� �ݱ�
        switches.OnSwitch += UnlockDoor; // ����ġ ����Ʈ
        door.OnOpenDoor += ClearStage; // �� ����
    }

    public IEnumerator StartResearchScene()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetIMacCamera();
        subtitle.ShowSubtitle("ī������ : ���� ��ǻ�Ͱ� �־�!  �� ��� ���� �ᰡ����!", delayTime: 1f) ;

        yield return new WaitForSeconds(6.5f);

        itemWheel.SetActive(true);
        controllerUI.ShowUI();
        cameraController.SetPlayCamera();
        inputHandler.gameObject.SetActive(true);
    }

    public void ShutBarrier()
    {
        inputHandler.gameObject.SetActive(false);
        StartCoroutine(barrier.MoveBarrierCoroutine());
        cat.SetActive(true);
        subtitle.ShowSubtitle("ī������ : ����! ��� ���� ��ɾ�!", delayTime: 2.5f) ;
    }

    public void ShutDoor()
    {
        controllerUI.RemoveUI();
        cameraController.SetDoorCamera();
        StartCoroutine(door.CloseDoor());
        subtitle.ShowSubtitle("ī������ : �̷�! �츮�� �ִ� ����� ����ݾ�!");
    }

    public void OnSwitch()
    {
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        subtitle.ShowSubtitle("ī������ : ���� �ִ� ����ġ�� �̿��ϸ� �� �� ������?");
        questUI.ActivatePopUP("����ġ �۵�", "�������� ����ġ�� ������");
        switches.InitSwitch();
        itemWheel.SetActive(true);
        controllerUI.ShowUI();
    }

    public void UnlockDoor()
    {
        questUI.DeactivatePopUp();
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetDoorCamera();
        StartCoroutine(door.OpenDoor());
        subtitle.ShowSubtitle("ī������ : �س´�! ���� ���Ⱦ�!", delayTime : 1f);
    }

    public void ClearStage()
    {
        itemWheel.SetActive(true);
        controllerUI.ShowUI();
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        subtitle.ShowSubtitle("ī������ : CCTV�� ���� ��Ű�� ��ġ�� ������!", delayTime: 0.5f);
        questUI.ActivatePopUP("�̵��ϱ�", "���� ������ �̵��Ͻʽÿ�");
        endPoint.SetActive(true);
    }

    public void RemovalVirus()
    {
        questUI.ActivatePopUP("���̷��� óġ", "���� ���� ���̷����� ��������");
    }

    public void GoUp()
    {
        questUI.DeactivatePopUp();
        questUI.ActivatePopUP("�ö󰡱�", "�ڽ��� Ÿ�� Ź�� ���� �ö󰡺���");
    }

    public void UpTable()
    {
        questUI.DeactivatePopUp();
    }
}
