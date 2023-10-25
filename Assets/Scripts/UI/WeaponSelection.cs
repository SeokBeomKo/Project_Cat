using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public GameObject weaponPopUP; // 무기 선택창 팝업
    private bool isActive = false;

    public delegate void SelectWeapons(string message);
    public static SelectWeapons onSelected = null;

    public string[] weaponList; // 무기 목록

    void Start()
    {
        weaponPopUP.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) // 마우스 휠 버튼 무기창 활성화
        {
            isActive = !isActive;
            weaponPopUP.SetActive(isActive);
        }
    }

    private void SelectWeapon(string weaponName)
    {
        Debug.Log(weaponName + " 선택 ");
    }
}

