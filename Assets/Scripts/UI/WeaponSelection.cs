using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    private Transform weaponContainer; // 무기 컨테이너

    void Start()
    {
        // 게임 시작 시 초기 무기 선택
        weaponContainer = gameObject.transform;
        SelectWeapon(2);
    }

    void Update()
    {
        // 키(예: 숫자 1, 2, 3)를 눌러 무기 변경
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            SelectWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            SelectWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3");
            SelectWeapon(2);
        }
    }

    void SelectWeapon(int weaponNum)
    {
        // 선택한 무기를 부모 컨테이너의 가장 아래로 이동
        weaponContainer.GetChild(weaponNum).GetComponent<RectTransform>().anchoredPosition = new Vector3(50, -250, 0);


        int j = 1;

        // 모든 무기 위치 재정렬
        for (int i = 0; i < weaponContainer.childCount; i++)
        {
            if (i == weaponNum) continue;

            weaponContainer.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(50, -250 + j * 100, 0);

            j++;
        }
    }


}


            /*Transform child = weaponContainer.GetChild(i);
            Vector3 newPosition = Vector3.up;
            newPosition.y = Vector3.up.y * i * 100 - 300; // 무기를 위아래로 등간격으로 배치
            child.localPosition = newPosition;*/