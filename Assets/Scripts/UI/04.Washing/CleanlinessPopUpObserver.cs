using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanlinessPopUpObserver : MonoBehaviour, IObserver
{
    // 팝업 창
    [Header("세척도 팝업창")]
    public GameObject popUp;

    public UIController uiController;

    // 팝업 창 텍스트
    [Header("상체")]
    public TextMeshProUGUI upperBody;
    [Header("하체")]
    public TextMeshProUGUI lowerBody;
    [Header("앞발")]
    public TextMeshProUGUI forePawRight;
    public TextMeshProUGUI forePawLeft;
    [Header("뒷발")]
    public TextMeshProUGUI rearPawRight;
    public TextMeshProUGUI rearPawLeft;
    [Header("등")]
    public TextMeshProUGUI back;

    public GameObject pause;

    public void Notify(ISubject subject)
    {
        CleanCat(subject as CatStatsSubject);
    }

    void Start()
    {
        popUp.SetActive(false);
    }

    public void ActivateCleanliness()
    {
        if (PlayerPrefs.GetInt("Pause") == 1) return;

        SoundManager.Instance.PlaySFX("Hover");
        popUp.SetActive(true);
        uiController.RemoveUI();
    }

    public void DeactivateCleanliness()
    {
        popUp.SetActive(false);
        if(!pause.activeSelf)
            uiController.ShowUI();
    }

    public void CleanCat(CatStatsSubject subject)
    {
        upperBody.text = "상체 : " + (subject.GetPartsCleanliness(PartsEnums.UPPERBODY) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        lowerBody.text = "하체 : " + (subject.GetPartsCleanliness(PartsEnums.LOWERBODY) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        rearPawRight.text = "뒷발 : " + (subject.GetPartsCleanliness(PartsEnums.REARPAWRIGHT) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        rearPawLeft.text = "뒷발 : " + (subject.GetPartsCleanliness(PartsEnums.REARPAWLEFT) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        forePawRight.text = "앞발 : " + (subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        forePawLeft.text = "앞발 : " + (subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
        back.text = "등 : " + (subject.GetPartsCleanliness(PartsEnums.BACK) / subject.currentMaxCleanliness * 100).ToString("00") + "%";
    }
}
