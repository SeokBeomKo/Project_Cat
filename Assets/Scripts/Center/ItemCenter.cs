using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("�÷��̾� ������Ʈ")]
    public GameObject Player;

    [Header("������ �� UI")]
    public ItemWheel itemWheel;
    
    [Header("��ȣ ������")]
    public ProtectEnergyUse energyUse;
    
    [Header("��")]
    public HairBallUse hairBallUse;

    [Header("���� ������")]
    public RandomItem randomItem;

    [Header("�÷��̾� ����")]
    public PlayerStats playerStats;

    [Header("�ѱ�")]
    public WeaponStrategy weaponStrategy;

    [Header("�̵� �ӵ� ���� ���� �ð�")]
    public float moveSpeedTime;

    [Header("���ݷ� ���� ���� �ð�")]
    public float attackPowerTime;

    public WeaponStrategy weapon;
    public GameObject bigBottleObject;
    private BigBottle bigBottle;
    public GameObject bottleObject;
    private NavigateVaseOperation bottle;


    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;
        if (randomItem != null)
            randomItem.OnRandomItem += GetRandomItem;

        bigBottle.OnChargeAll += ChargeAll;

        bigBottle = GetComponentInChildren<BigBottle>();
        bottle = GetComponentInChildren<NavigateVaseOperation>();
    }

    public void ClickTrue(string itemName)
    {
        switch(itemName)
        {
            case "��ȣ��":
                energyUse.CreateProtectEnergy(Player.transform.position, Player.transform);
                break;

            case "�̵��ӵ�":
                playerStats.AddMoveSpeed(moveSpeedTime);
                break;

            case "���ݷ�":
                weaponStrategy.DamageUp(attackPowerTime);
                break;

            case "�й�ġ":
                if (!hairBallUse.CheckObstacleInFront(Player.transform.position, Player.transform.forward))
                {
                    hairBallUse.CreateHairBall(Player.transform.position, Player.transform.forward);
                }
                else
                {
                    Debug.Log("[ItemCenter] ��ֹ��� �־� ������ �� ����");
                }
                break;

            case "��":
                Debug.Log("[ItemCenter] �� ����");
                break;
        }
    }

    public void GetRandomItem(string itemName)
    {
        switch(itemName)
        {
            case "WaterBottle":
                Debug.Log("[ItemCenter] ź�� ����");
                break;

            case "LifeEnergy":
                playerStats.FillHealth(10); // ��ġ �����ؾ� ��
                Debug.Log("[ItemCenter] �÷��̾� HP ����");
                break;
        }
    }

    public void ChargeAll()
    {
        weapon.ChargeAllBullet();
    }

    public void ChargeCurrent()
    {
        weapon.ChargeCurrentBullet(bottle.GetChargeAmount());
    }
}
