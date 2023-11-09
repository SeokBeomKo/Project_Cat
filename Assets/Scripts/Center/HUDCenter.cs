using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCenter : MonoBehaviour
{
    [Header("플레이어 스탯")]
    [SerializeField] public PlayerStats playerStats;
    [Header("플레이어 HUD")]
    [SerializeField] public HPObserver hpObserver;
    [SerializeField] public RollObserver rollObserver;

    private void Start() 
    {
        playerStats.AddObserver<IObserver>(playerStats.hpObserverList,hpObserver);
        playerStats.AddObserver<IObserver>(playerStats.rollObserverList,rollObserver);
    }

    public void HitHp()
    {

    }
}
