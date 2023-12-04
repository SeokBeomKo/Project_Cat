using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CleanlinessProgressObserver : MonoBehaviour, IObserver
{
    public Slider cleanlinessProgressBar;
    public TextMeshProUGUI cleanlinessText;

    private float totalClean = 700;
    public float currentClean;

    public void Notify(ISubject subject)
    {
        UpdateProgress(subject as CatStatsSubject);
    }


    public void UpdateProgress(CatStatsSubject catStats)
    {
        currentClean = catStats.GetTotalCleanliness();
        cleanlinessProgressBar.value = Mathf.Clamp01(currentClean / (catStats.currentMaxCleanliness * 7));
        cleanlinessText.text = Mathf.RoundToInt(cleanlinessProgressBar.value * 100) + " %";
    }
}
