using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanlinessPopUpObserver : MonoBehaviour, IObserver
{
    // ÆË¾÷ Ã¢
    [Header("¼¼Ã´µµ ÆË¾÷Ã¢")]
    public GameObject popUp;

    // ÆË¾÷ Ã¢ ÅØ½ºÆ®
    [Header("»óÃ¼")]
    public TextMeshProUGUI upperBody;
    [Header("ÇÏÃ¼")]
    public TextMeshProUGUI lowerBody;
    [Header("¾Õ¹ß")]
    public TextMeshProUGUI forePawRight;
    public TextMeshProUGUI forePawLeft;
    [Header("µÞ¹ß")]
    public TextMeshProUGUI rearPawRight;
    public TextMeshProUGUI rearPawLeft;
    [Header("µî")]
    public TextMeshProUGUI back;

    // Äµ¹ö½º 
    [Header("¼û±æ UI")]
    public CanvasGroup canvas;

    public void Notify(ISubject subject)
    {
        CleanCat(subject as CatStatsSubject);
    }

    void Start()
    {
        popUp.SetActive(false);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Pause") == 1) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SoundManager.Instance.PlaySFX("Hover");
            popUp.SetActive(true);
            RemoveUI();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            popUp.SetActive(false);
            ShowUI();
        }
        
    }

    public void CleanCat(CatStatsSubject subject)
    {
        upperBody.text = "»óÃ¼ : " + (subject.GetPartsCleanliness(PartsEnums.UPPERBODY) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        lowerBody.text = "ÇÏÃ¼ : " + (subject.GetPartsCleanliness(PartsEnums.LOWERBODY) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        rearPawRight.text = "µÞ¹ß : " + (subject.GetPartsCleanliness(PartsEnums.REARPAWRIGHT) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        rearPawLeft.text = "µÞ¹ß : " + (subject.GetPartsCleanliness(PartsEnums.REARPAWLEFT) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        forePawRight.text = "¾Õ¹ß : " + (subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        forePawLeft.text = "¾Õ¹ß : " + (subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT) / subject.currentMaxLikeability * 100).ToString("00") + "%";
        back.text = "µî : " + (subject.GetPartsCleanliness(PartsEnums.BACK) / subject.currentMaxLikeability * 100).ToString("00") + "%";
    }

    public void RemoveUI()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void ShowUI()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}
