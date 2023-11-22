using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStiffenState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.IDLE,
        PlayerMovementStateEnums.MOVE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerStiffenState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")&&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    stateMachine.ChangeStateLogic(PlayerMovementStateEnums.MOVE);
                    return;
                }
                else
                {
                    stateMachine.ChangeStateLogic(PlayerMovementStateEnums.IDLE);
                    return;
                }
            }
    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        player.animator.SetTrigger("onHit");
    }

    public void OnStateExit()
    {
        player.rigid.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetAimCamera(false);
    }
}