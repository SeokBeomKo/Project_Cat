using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponCenter : MonoBehaviour
{
    public List<Weapon> weaponList;
    private Weapon curWeapon;

    private void Start()
    {
        InitWeapon();
        SwapWeapon(0);
    }

    public void InitWeapon()
    {
        foreach(Weapon obj in weaponList)
        {
            obj.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SwapWeapon(int number)
    {
        if (null != curWeapon)
            curWeapon.transform.parent.gameObject.SetActive(false);

        curWeapon = weaponList[number];
        curWeapon.transform.parent.gameObject.SetActive(true);
    }

    public void FireWeapon()
    {
        // 화면 중앙(크로스헤어 위치)를 월드 좌표로 변환합니다.
        Camera activeCamera = Camera.main; // 메인 카메라를 참조합니다.
        Ray ray = activeCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        // Camera activeCamera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<Camera>();
        // Ray ray = activeCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point; // 레이가 부딪힌 위치를 타겟 포인트로 설정합니다.
        else
            targetPoint = ray.GetPoint(50); // 레이가 부딪히지 않았다면, 일정 거리를 타겟 포인트로 설정합니다.

        curWeapon.Fire(targetPoint);
    }
}
