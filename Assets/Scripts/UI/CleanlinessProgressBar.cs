using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CleanlinessProgressBar : MonoBehaviour
{
    public Slider cleanlinessProgressBar;
    public TextMeshProUGUI cleanlinessText;

    private float totalClean = 700;
    public float currentClean;

    void Start()
    {
        currentClean = 0;
    }

    public void UpdateProgress(float current)
    {
        currentClean = current;
        cleanlinessProgressBar.value = Mathf.Clamp01(currentClean / totalClean);
        cleanlinessText.text = Mathf.RoundToInt(cleanlinessProgressBar.value * 100) + " %";
    }
}
