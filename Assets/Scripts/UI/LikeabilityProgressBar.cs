using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LikeabilityProgressBar : MonoBehaviour
{
    public Slider likeabilityProgressBar;
    public TextMeshProUGUI likeabilityText;

    private float totalLikeability = 300;
    private float currentLikeability;

    /*public void Notify(ISubject subject)
    {
        UpdateLikeabilityProgress(°í¾çÀÌ);
    }*/

    public void UpdateLikeabilityProgress(float current)
    {
        currentLikeability = current;
        likeabilityProgressBar.value = Mathf.Clamp01(currentLikeability / totalLikeability);
        likeabilityText.text = likeabilityProgressBar.value * 100 + " %";
    }
}
