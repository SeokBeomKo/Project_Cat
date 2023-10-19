using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.DOUBLE,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.IDLE,
    };
    
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerFallState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.rigid.velocity.y >= 0)
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.IDLE);
            return;
        }
    }


    public void OnStateEnter()
    {
        player.animator.SetBool("isFall", true);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isFall", false);
    }
}
