using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPopUp : MonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questText;

    public void ActivatePopUP(string title, string content)
    {
        questTitleText.text = title;
        questText.text = content;
        gameObject.SetActive(true);
    }

    public void DeactivatePopUp()
    {
        gameObject.SetActive(false);
    }
}
