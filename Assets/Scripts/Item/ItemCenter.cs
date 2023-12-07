using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어")]
    public GameObject Player;

    [Header("아이템 휠 UI")]
    public ItemWheel itemWheel;
    
    [Header("보호 에너지")]
    public ProtectEnergyUse energyUse;
    
    [Header("헤어볼")]
    public HairBallUse hairBallUse;

    [Header("랜덤 아이템")]
    public RandomItem randomItem;

    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;
        randomItem.OnRandomItem += GetRandomItem;
    }

    public void ClickTrue(string itemName)
    {
        switch(itemName)
        {
            case "보호막":
                energyUse.CreateProtectEnergy(Player.transform.position, Player.transform);
                Debug.Log("[ItemCenter] 보호에너지 선택");
                break;

            case "이동속도":
                Debug.Log("[ItemCenter] 운동에너지 - 이동속도 선택");
                break;

            case "공격력":
                Debug.Log("[ItemCenter] 운동에너지 - 공격력 선택");
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
                Debug.Log("[ItemCenter] 털뭉치 선택");
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
