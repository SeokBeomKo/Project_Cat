using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanlinessPopUpObserver : MonoBehaviour, IObserver
{
    // ÆË¾÷ Ã¢
    public GameObject popUp;

    // ÆË¾÷ Ã¢ ÅØ½ºÆ®
    public TextMeshProUGUI upperBody;
    public TextMeshProUGUI lowerBody;
    public TextMeshProUGUI rearPawRight;
    public TextMeshProUGUI rearPawLeft;
    public TextMeshProUGUI forePawRight;
    public TextMeshProUGUI forePawLeft;
    public TextMeshProUGUI back;

    // Äµ¹ö½º 
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
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            popUp.SetActive(false);
        }
        
    }

    public void CleanCat(CatStatsSubject subject)
    {
        upperBody.text = "»óÃ¼ : " + subject.GetPartsCleanliness(PartsEnums.UPPERBODY).ToString("00") + "%";
        lowerBody.text = "ÇÏÃ¼ : " + subject.GetPartsCleanliness(PartsEnums.LOWERBODY).ToString("00") + "%";
        rearPawRight.text = "¾Õ¹ß : " + subject.GetPartsCleanliness(PartsEnums.REARPAWRIGHT).ToString("00") + "%";
        rearPawLeft.text = "¾Õ¹ß : " + subject.GetPartsCleanliness(PartsEnums.REARPAWLEFT).ToString("00") + "%";
        forePawRight.text = "µÞ¹ß : " + subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT).ToString("00") + "%";
        forePawLeft.text = "µÞ¹ß : " + subject.GetPartsCleanliness(PartsEnums.FOREPAWRIGHT).ToString("00") + "%";
        back.text = "µî : " + subject.GetPartsCleanliness(PartsEnums.BACK).ToString("00") + "%";
    }
}
