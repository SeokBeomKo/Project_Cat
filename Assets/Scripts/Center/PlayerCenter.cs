using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    
    [SerializeField]
    PlayerHitScan playerHitScan;

    [SerializeField]
    PlayerStats playerStats;

    private void Start() 
    {
        playerHitScan.OnPlayerHitScan += HitPlayer;
    }

    public void HitPlayer(int damage = 5)
    {
        playerController.Hit();
        playerController.stateMachine.ChangeStateAny(PlayerStateEnums.STIFFEN);
        playerStats.GetDamage(damage);
    }
}
