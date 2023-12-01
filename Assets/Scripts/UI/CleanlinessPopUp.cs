using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanlinessPopUp : MonoBehaviour
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

    void Start()
    {
        popUp.SetActive(false);
    }

    void Update()
    {
        //popUp.SetActive(false);

        if (PlayerPrefs.GetInt("Pause") == 1) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            popUp.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            popUp.SetActive(false);
        }
        
    }

    public void CleanCat(CleanEnums parts,  float cleanliness)
    {
        switch(parts)
        {
            case CleanEnums.UPPERBODY:
                upperBody.text = "»óÃ¼ : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.LOWERBODY:
                lowerBody.text = "ÇÏÃ¼ : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.REARPAWRIGHT:
                rearPawRight.text = "¾Õ¹ß : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.REARPAWLEFT:
                rearPawLeft.text = "¾Õ¹ß : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.FOREPAWRIGHT:
                forePawRight.text = "µÞ¹ß : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.FOREPAWLEFT:
                forePawLeft.text = "µÞ¹ß : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.BACK:
                back.text = "µî : " + cleanliness.ToString("00") + "%";
                break;
                
        }
    }
}
