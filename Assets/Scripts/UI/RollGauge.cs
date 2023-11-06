using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollGauge : MonoBehaviour
{
    public int maxGauge = 3;
    private int currentGauge;
    public Image[] gaugeImageList;


    void Start()
    {
        currentGauge = maxGauge;
    }
    void Update()
    {
        IncreaseGauge();

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            DecreaseGauge();
        }
    }

    private void DecreaseGauge()
    {
        if (currentGauge > 0)
        {
            currentGauge--;
            UpdateImage();
        }
    }

    private void IncreaseGauge()
    {

            StartCoroutine(Delaytime());
    }

    private IEnumerator Delaytime()
    {
        yield return new WaitForSeconds(3);

        if (currentGauge < maxGauge)
        {
            currentGauge++;
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        // 모든 게이지 이미지를 비활성화
        for (int i = 0; i < gaugeImageList.Length; i++)
        {
            gaugeImageList[i].enabled = false;
        }

        // 현재 게이지 값에 따라 해당 게이지 이미지 활성화
        for (int i = 0; i < currentGauge; i++)
        {
            gaugeImageList[i].enabled = true;
        }
    }
}
