using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollGauge : MonoBehaviour
{
    public Image[] gaugeImageArray;
    //public Image progressBar;
    //public TextMeshProUGUI gaugeNumber;

    private int maxGauge = 3;
    private int currentGauge;

    private float charginTime = 3.0f;
    private float lastDecreaseTime;

    void Start()
    {
        currentGauge = maxGauge;
        lastDecreaseTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DecreaseGauge();
        }

        if (Time.time - lastDecreaseTime >= charginTime && currentGauge < maxGauge)
        {
            IncreaseGauge();
        }
    }

    private void DecreaseGauge()
    {
        if (currentGauge > 0)
        {
            currentGauge--;
            lastDecreaseTime = Time.time;
            UpdateImage();
        }
    }

    private void IncreaseGauge()
    {
        if (currentGauge < maxGauge)
        {
            currentGauge++;
            lastDecreaseTime = Time.time;
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        for (int i = 0; i < gaugeImageArray.Length; i++)
        {
            if (i < currentGauge)
                gaugeImageArray[i].enabled = true; 
            else
                gaugeImageArray[i].enabled = false; 
        }
    }
}
