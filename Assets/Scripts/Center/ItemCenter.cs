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

    public GameObject bigBottleObject;
    public BigBottle[] bigBottle;

    public GameObject bottleObject;
    public NavigateVaseOperation[] bottle;


    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;
        if (randomItem != null)
            randomItem.OnRandomItem += GetRandomItem;

        bigBottle = bigBottleObject.GetComponentsInChildren<BigBottle>();
        bottle = bottleObject.GetComponentsInChildren<NavigateVaseOperation>();

        if (bigBottle != null)
        {
           for(int i=0; i<bigBottle.Length; i++)
            {
                int index = i;
                bigBottle[index].OnChargeAll += GetRandomItem;
            }
        }

        if (bottle != null)
        {
            for (int i = 0; i < bottle.Length; i++)
            {
                int index = i;
                bottle[index].OnCharge += GetRandomItem;
            }
        }
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
        Debug.Log(" function call");

        switch (itemName)
        {
            case "WaterBottle":
                weaponStrategy.ChargeCurrentBullet(bottle[0].GetChargeAmount());
                break;

            case "LifeEnergy":
                playerStats.FillHealth(10); // ��ġ �����ؾ� ��
                break;

            case "BigBottle":
                Debug.Log("item all charge function call");

                weaponStrategy.ChargeAllBullet();
                break;
        }
    }
}
