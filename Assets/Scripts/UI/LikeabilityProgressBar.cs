using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LikeabilityProgressBar : MonoBehaviour
{
    public Slider likeabilityProgressBar;
    public TextMeshProUGUI likeabilityText;

    private float likeability = 300;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (likeability > 0)
            {
                likeability -= 30;
                likeabilityProgressBar.value = Mathf.Clamp01(likeability / 300);

                likeabilityText.text = likeabilityProgressBar.value * 100 + " %";
            }
        }
    }
}
