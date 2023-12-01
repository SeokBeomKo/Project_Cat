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
        playerHitScan.OnPlayerSpeedUp += SpeedUp;
        playerHitScan.OnPlayerDamageUp += DamageUp;
    }

    public void DamageUp()
    {
        
    }

    public void SpeedUp()
    {
        playerStats.AddMoveSpeed(5);
    }

    public void HitPlayer(int damage = 5)
    {
        playerStats.GetDamage(damage);

        if (0 >= playerStats.currentHealth)
        {
            playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.DEAD);
        }
        else
        {
            playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.STIFFEN);
        }
    }
}
