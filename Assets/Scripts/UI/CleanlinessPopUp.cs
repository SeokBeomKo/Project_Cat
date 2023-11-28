using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanlinessPopUp : MonoBehaviour
{
    // 팝업 창
    public GameObject popUp;

    // 팝업 창 텍스트
    public TextMeshProUGUI upperBody;
    public TextMeshProUGUI lowerBody;
    public TextMeshProUGUI rearPawRight;
    public TextMeshProUGUI rearPawLeft;
    public TextMeshProUGUI forePawRight;
    public TextMeshProUGUI forePawLeft;
    public TextMeshProUGUI back;

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
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            popUp.SetActive(false);
        }

    }

    public void CleanCat(CleanEnums parts,  float cleanliness)
    {
        switch(parts)
        {
            case CleanEnums.UPPERBODY:
                upperBody.text = "상체 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.LOWERBODY:
                lowerBody.text = "하체 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.REARPAWRIGHT:
                rearPawRight.text = "앞발 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.REARPAWLEFT:
                rearPawLeft.text = "앞발 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.FOREPAWRIGHT:
                forePawRight.text = "뒷발 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.FOREPAWLEFT:
                forePawLeft.text = "뒷발 : " + cleanliness.ToString("00") + "%";
                break;
            case CleanEnums.BACK:
                back.text = "등 : " + cleanliness.ToString("00") + "%";
                break;
                
        }
    }
}
