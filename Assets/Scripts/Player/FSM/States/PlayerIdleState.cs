using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.RUN,
        PlayerStateEnums.JUMP,
        PlayerStateEnums.DIVEROLL
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
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
        // if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        // {
        //     stateMachine.ChangeState(PlayerStateEnums.RUN);
        //     return;
        // }
        // if (Input.GetButtonDown("Jump"))
        // {
        //     stateMachine.ChangeState(PlayerStateEnums.JUMP);
        //     return;
        // }
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }
}