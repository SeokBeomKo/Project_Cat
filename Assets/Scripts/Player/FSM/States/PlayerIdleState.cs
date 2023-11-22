using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.MOVE,
        PlayerStateEnums.JUMP,
        PlayerStateEnums.DIVEROLL,
        PlayerStateEnums.BACKROLL,

        PlayerStateEnums.AIM,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
    };

    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerIdleState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (!player.CheckGrounded())
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.FALL);
            return;
        }
    }

    public void OnStateEnter()
    {
        player.playerStats.FillDoubleCount();
    }

    public void OnStateExit()
    {
    }
}