using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어 오브젝트")]
    public GameObject Player;

    [Header("아이템 휠 UI")]
    public ItemWheel itemWheel;
    
    [Header("보호 에너지")]
    public ProtectEnergyUse energyUse;
    
    [Header("헤어볼")]
    public HairBallUse hairBallUse;

    [Header("랜덤 아이템")]
    public RandomItem randomItem;

    [Header("플레이어 스탯")]
    public PlayerStats playerStats;

    [Header("총기")]
    public WeaponStrategy weaponStrategy;

    [Header("이동 속도 증가 지속 시간")]
    public float moveSpeedTime;

    [Header("공격력 증가 지속 시간")]
    public float attackPowerTime;


    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;
        if (randomItem != null)
            randomItem.OnRandomItem += GetRandomItem;
    }

    public void ClickTrue(string itemName)
    {
        switch(itemName)
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
                else
                {
                    Debug.Log("[ItemCenter] 장애물이 있어 생성할 수 없음");
                }
                break;

            case "츄르":
                Debug.Log("[ItemCenter] 츄르 선택");
                break;
        }
    }

    public void GetRandomItem(string itemName)
    {
        switch(itemName)
        {
            case "WaterBottle":
                Debug.Log("[ItemCenter] 탄약 충전");
                break;
            case "LifeEnergy":
                Debug.Log("[ItemCenter] 플레이어 HP 충전");
                break;
        }
    }
}
