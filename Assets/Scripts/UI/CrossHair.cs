using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private RectTransform crossHair;

    public float restingSize; // �������� ���� ���� ũ��
    public float aimSize;  // ���� �� ũ��
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

