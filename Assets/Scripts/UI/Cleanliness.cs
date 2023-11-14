using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cleanliness : MonoBehaviour
{
    public Slider cleanlinessProgressBar;
    public TextMeshProUGUI cleanlinessText;

    private float cleanliness = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (cleanliness < 100)
            {
                cleanliness += 10;
                cleanlinessProgressBar.value = Mathf.Clamp01(cleanliness / 100);

                cleanlinessText.text = cleanlinessProgressBar.value * 100 + " %";
            }
        }
    }
}
