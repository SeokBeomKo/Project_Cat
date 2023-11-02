/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWheel : MonoBehaviour
{
    //...

    private Vector3 normalScale = new Vector3(1, 1, 1);
    private Vector3 enlargedScale = new Vector3(1.3f, 1.3f, 1.3f);

    // Start is called before the first frame update
    void Start()
    {
        DeactivateMenu();
        DeactivateKineticEnergyMenu();
    }

    //...

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            hasRightMouseClicked = false;
        }

        if (Input.GetMouseButtonDown(2)) // 마우스 휠 버튼 무기창 활성화
        {
            isActive = !isActive;
            if (isActive && !isMenuActive)
                ActivateMenu();
            else
                DeactivateMenu();
        }

        if (isActive)
        {
            float mouseDistance = Vector3.Distance(Input.mousePosition, center.position);
            float itemMaxDistance = Vector3.Distance(itemMax.position, center.position);
            float itemMinDistance = Vector3.Distance(itemMin.position, center.position);

            // 중앙으로부터의 마우스 거리가 경계값 안에 있는지 확인
            if (mouseDistance < itemMaxDistance && mouseDistance > itemMinDistance)
            {
                // ...
            }
            else
            {
                ResetItemName();
                ResetItemSlots();
            }

            if (Input.GetMouseButtonDown(1))
            {
                DeactivateMenu();
            }
        }

        if (isMenuActive)
        {
            if (Input.GetMouseButtonDown(1))
            {
                DeactivateKineticEnergyMenu();
                ActivateMenu();
            }
            else if (Input.GetMouseButtonDown(2))
            {
                DeactivateKineticEnergyMenu();
                DeactivateMenu();
            }

            // ...
        }
    }

    void ActivateMenu()
    {
        Debug.Log(isActive);
        itemMenu.SetActive(true);
    }

    void DeactivateMenu()
    {
        Debug.Log("선택 취소");
        isActive = false;
        itemMenu.SetActive(false);
    }

    void ActivateKineticEnergyMenu()
    {
        isMenuActive = true;
        kineticEnergyMenu.SetActive(true);
    }

    void DeactivateKineticEnergyMenu()
    {
        Debug.Log("운동 에너지 선택 취소");
        isMenuActive = false;
        kineticEnergyMenu.SetActive(false);
    }

    void ResetItemName()
    {
        itemName.text = "MENU";
        itemExplanation.text = " ";
    }

    void ResetItemSlots()
    {
        foreach (Transform t in itemSlotArray)
        {
            t.transform.localScale = normalScale; // 모든 이미지 크기 (1, 1, 1)로 설정
        }
    }
}


*/