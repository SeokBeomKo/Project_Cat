using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : Weapon
{
    [Header("발사 위치")]
    public Transform shootPosition;

    [Header("발사체 프리팹")]
    public GameObject projectilePrefab; // 발사체 프리팹

    [Header("진행 방향")]
    private Vector3 shootDirection;

    [Header("발사 딜레이")]
    public float shootDelay;
    private float curShootTime;

    public override void EnterShoot()
    {
        curShootTime = 0;
    }
    public override void ExcuteShoot()
    {
        curShootTime -= Time.deltaTime;
        if (curShootTime <= 0)
        {
            curShootTime = shootDelay;
            Shoot();
        }
    }

    public override void ExitShoot()
    {
    }

    public override void SetTarget(Vector3 direction)
    {
        shootDirection = direction;
    }
    private float randomScale;

    public override void Shoot()
    {
        randomScale = Random.Range(0.2f,0.7f);
        // 비눗방울을 발사합니다.
        GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
        bullet.GetComponentInChildren<BubbleProjectile>().SetDirection(shootDirection);
        //
    }
}
