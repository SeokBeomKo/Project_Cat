using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CleanlinessProgressObserver : MonoBehaviour, IObserver
{
    public Slider cleanlinessProgressBar;
    public TextMeshProUGUI cleanlinessText;

    private float totalClean;
    private float currentClean;

    public void Notify(ISubject subject)
    {
        UpdateProgress(subject as CatStatsSubject);
    }


    public void UpdateProgress(CatStatsSubject catStats)
    {
        currentClean = catStats.GetTotalCleanliness();
        totalClean = catStats.GetTotalMaxCleanliness();
        cleanlinessProgressBar.value = Mathf.Clamp01(currentClean / totalClean);
        cleanlinessText.text = Mathf.RoundToInt(cleanlinessProgressBar.value * 100) + " %";

        if(cleanlinessProgressBar.value == 1)
        {
            SceneManager.LoadScene("HappyEnding");
        }
    }
}
