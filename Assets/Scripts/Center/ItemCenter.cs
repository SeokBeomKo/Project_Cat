using System;
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
    private RandomItem[] randomItem;
    public GameObject randomItemParent;

    [Header("아이템")]
    public Item[] itemArray;
    public GameObject ItemParent;

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
    private BigBottle[] bigBottle;

    public GameObject bottleObject;
    private NavigateVaseOperation[] bottle;

    public GameObject CapsuleNavigateParent;
    private CapsuleNavigateOperation[] CapsuleNavigates;

    public GameObject CapsuleWashingParent;
    private CapsuleWashingOperation[] CapsuleWashing;

    public BattleCenter battleCenter;

    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;

        if (CapsuleNavigateParent != null)
        {
            CapsuleNavigates = CapsuleNavigateParent.GetComponentsInChildren<CapsuleNavigateOperation>();
        }

        if(CapsuleNavigates != null)
        {
            for (int i = 0; i < CapsuleNavigates.Length; i++)
                CapsuleNavigates[i].onItemCreate += CreateItem;
        }

        if(CapsuleWashingParent != null)
        {
            CapsuleWashing = CapsuleWashingParent.GetComponentsInChildren<CapsuleWashingOperation>();
        }

        if (CapsuleWashing != null)
        {
            for (int i = 0; i < CapsuleWashing.Length; i++)
                CapsuleWashing[i].onItemCreate += CreateItem;
        }

        if (battleCenter != null)
        {
            battleCenter.onItemCreate += CreateItem;
        }

        if(randomItemParent!= null)
        {
            randomItem = randomItemParent.GetComponentsInChildren<RandomItem>();
        }

        if (randomItem != null)
        {
            for(int i = 0; i < randomItem.Length; i++)
                randomItem[i].OnRandomItem += GetRandomItem;
        }


        if (ItemParent != null)
        {
            itemArray = ItemParent.GetComponentsInChildren<Item>();
        }
        else
        {
        itemArray = new Item[0];

        }

        if (itemArray != null)
        {
            for(int i = 0; i < itemArray.Length; i++)
            {
                itemArray[i].OnItem += GetItems;
            }
        }

        if(bigBottleObject != null)
        {
            bigBottle = bigBottleObject.GetComponentsInChildren<BigBottle>();

        }


        if(bottleObject != null)
        {
            bottle = bottleObject.GetComponentsInChildren<NavigateVaseOperation>();

        }

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
        Debug.Log(itemName);
        getItem.gameObject.SetActive(true);
        GetDirectItem(itemName);
        StartCoroutine(getItem.ShowGetItemText(itemName));
    }
    public void GetItems(string itemName)
    {
        Debug.Log("먹어짐222222 " + itemName);

        getItem.gameObject.SetActive(true);

        GetDirectItem(itemName);
        StartCoroutine(getItem.ShowGetItemText(itemName));
    }

    public void CreateItem(Item item)
    {
        Debug.Log("[아이템센터] Create Item " + item.name);

         if (item.name.EndsWith("(Clone)"))
            {
                item.name = item.name.Substring(0, item.name.Length - 7); // 7은 "(Clone)" 문자열의 길이입니다.
            }
        Array.Resize(ref itemArray, itemArray.Length + 1);
        itemArray[itemArray.Length - 1] = item;
        itemArray[itemArray.Length - 1].OnItem += GetItems;
    }

    private void GetDirectItem(string itemName)
    {
        Debug.Log("먹어짐 " + itemName);
        

        switch (itemName)
        {
            
            case "WaterBottle":
                Debug.Log("사용됨 " + itemName);

                weaponStrategy.ChargeCurrentBullet(bottle[0].GetChargeAmount());
                break;

            case "생명에너지":
                Debug.Log("+10");
                playerStats.FillHealth(10);
                break;

            case "BigBottle":
                weaponStrategy.ChargeAllBullet();
                break;
            case "MoveSpeed":
                playerStats.AddMoveSpeed(moveSpeedTime);
                break;
        }
    }
}
