using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponStrategy : MonoBehaviour
{
    public delegate void WeaponHandle(int number);
    public event WeaponHandle OnSwapWeapon;

    public List<Weapon> weaponList;
    private Weapon curWeapon;

    public int damageOffset = 1;

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

    public void SetCurrentBullet(int soawpBullet, int splashBullet, int bubbleBullet)
    {
        weaponList[0].curBullet = soawpBullet;
        weaponList[1].curBullet = splashBullet;
        weaponList[2].curBullet = bubbleBullet;
    }

    public void ChargeCurrentBullet(int charge)
    {
        curWeapon.ChargeBullet(charge);
    }

    public void ChargeAllBullet()
    {
        foreach(Weapon obj in weaponList)
        {
            obj.ChargeAllBullet();
        }
    }

    public void DamageUp(float time)
    {
        damageOffset = 2;
        curWeapon.SetOffset(damageOffset);
        StartCoroutine(RecoveryDamage(time));
    }

    IEnumerator RecoveryDamage(float time)
    {
        yield return new WaitForSeconds(time);
        damageOffset = 1;
    }

    public void SwapWeapon(int number)
    {
        if (null != curWeapon)
            curWeapon.transform.parent.gameObject.SetActive(false);

        OnSwapWeapon?.Invoke(number);

        curWeapon = weaponList[number];
        curWeapon.transform.parent.gameObject.SetActive(true);
        curWeapon.SetOffset(damageOffset);
    }

    public void SettingTarget()
    {
        // 화면 중앙(크로스헤어 위치)를 월드 좌표로 변환합니다.
        Camera activeCamera = Camera.main; // 메인 카메라를 참조합니다.
        Ray ray = activeCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;
        // "PlayerAttack" 레이어의 LayerMask를 구합니다.
        int playerAttackLayer = LayerMask.NameToLayer("PlayerAttack");
        // 모든 레이어를 대상으로 하되 "PlayerAttack" 레이어는 제외합니다.
        int layerMask = ~(1 << playerAttackLayer);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            targetPoint = hit.point; // 레이가 부딪힌 위치를 타겟 포인트로 설정합니다.
        else
            targetPoint = ray.GetPoint(50); // 레이가 부딪히지 않았다면, 일정 거리를 타겟 포인트로 설정합니다.

        curWeapon.SetTarget(targetPoint);
    }

    public void EnterShoot()
    {
        SettingTarget();
        if (null != curWeapon) curWeapon.EnterShoot();
    }

    public void ExcuteShoot()
    {
        SettingTarget();
        if (null != curWeapon) curWeapon.ExcuteShoot();
    }

    public void ExitShoot()
    {
        SettingTarget();
        if (null != curWeapon) curWeapon.ExitShoot();
    }

    public void InitShoot()
    {
        if (null != curWeapon) curWeapon.InitShoot();
    }
}
