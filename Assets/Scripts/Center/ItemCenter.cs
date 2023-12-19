using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어")]
    public GameObject Player;

    [Header("아이템 휠 UI")]
    public ItemWheel itemWheel;

    [Header("보호에너지")]
    public ProtectEnergyUse energyUse;

    [Header("털뭉치")]
    public HairBallUse hairBallUse;

    [Header("랜덤 아이템")]
    public RandomItem[] randomItem;

    [Header("아이템")]
    public Item[] itemArray;

    [Header("플레이어 스탯")]
    public PlayerStats playerStats;

    [Header("무기")]
    public WeaponStrategy weaponStrategy;

    [Header("고양이")]
    public CatStatsSubject catSubject;

    [Header("이동속도 시간")]
    public float moveSpeedTime;

    [Header("공격력 시간")]
    public float attackPowerTime;

    [Header("아이템 출력 UI")]
    public GetItem getItem;

    public GameObject bigBottleObject;
    public BigBottle[] bigBottle;

    public GameObject bottleObject;
    public NavigateVaseOperation[] bottle;


    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;

        if (randomItem != null)
        {
            for(int i = 0; i < randomItem.Length; i++)
                randomItem[i].OnRandomItem += GetRandomItem;
        }

        if (itemArray != null)
        {
            for(int i = 0; i < itemArray.Length; i++)
            {
                itemArray[i].OnItem += GetItems;
            }
        }

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

        getItem.gameObject.SetActive(false);
    } 

    public void ClickTrue(string itemName)
    {
        switch (itemName)
        {
            case "보호막":
                energyUse.CreateProtectEnergy(Player.transform.position, Player.transform);
                break;

            case "이동속도":
                playerStats.AddMoveSpeed(moveSpeedTime);
                break;

            case "공격력":
                weaponStrategy.DamageUp(attackPowerTime);
                break;

            case "털뭉치":
                if (!hairBallUse.CheckObstacleInFront(Player.transform.position, Player.transform.forward))
                {
                    hairBallUse.CreateHairBall(Player.transform.position, Player.transform.forward);
                }
                 break;

            case "츄르":
                if(catSubject != null)
                    catSubject.IncreaseLikeability(30);
                break;
        }
    }

    public void GetRandomItem(string itemName)
    {
        Debug.Log(" function call");
        Debug.Log(itemName);
        getItem.gameObject.SetActive(true);
        StartCoroutine(getItem.ShowGetItemText(itemName));

        switch (itemName)
        {
            case "WaterBottle":
                weaponStrategy.ChargeCurrentBullet(bottle[0].GetChargeAmount());
                break;

            case "생명에너지":
                playerStats.FillHealth(10); 
                break;

            case "BigBottle":
                Debug.Log("item all charge function call");
                weaponStrategy.ChargeAllBullet();
                break;
        }
    }

    public void GetItems(string itemName)
    {
        getItem.gameObject.SetActive(true);
        StartCoroutine(getItem.ShowGetItemText(itemName));
    }
}
