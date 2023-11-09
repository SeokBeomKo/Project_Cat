using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private RectTransform crossHair;

    public float restingSize = 315; // 움직이지 않을 때의 크기
    public float aimSize = 240;  // 조준 시 크기
    public float speed;
    private float currentSize;

    private void Start()
    {
        crossHair = GetComponent<RectTransform>();

        crossHair.sizeDelta = new Vector2(restingSize, restingSize);
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            currentSize = Mathf.Lerp(currentSize, aimSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }
        crossHair.sizeDelta = new Vector2(currentSize, currentSize);
    }
}

