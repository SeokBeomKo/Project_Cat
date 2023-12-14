using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [Header("크로스 헤어")]
    public RectTransform crossHair;
    [Header("기본 사이즈")]
    public float restingSize;
    [Header("조준 시 사이즈")]
    public float aimSize;
    [Header("조준 속도")]
    public float speed;


    private float currentSize;

    private void Start()
    {
        crossHair = GetComponent<RectTransform>();
    }
    private void OnDisable() 
    {
        crossHair.sizeDelta = new Vector2(aimSize, aimSize);
        currentSize = 0;
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

