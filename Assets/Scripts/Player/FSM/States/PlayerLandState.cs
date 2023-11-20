using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.IDLE,
        PlayerStateEnums.MOVE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerLandState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("jump down") &&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                stateMachine.ChangeStateLogic(PlayerStateEnums.IDLE);
                return;
            }
            else
            {
                stateMachine.ChangeStateLogic(PlayerStateEnums.MOVE);
                return;
            }
        }
    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.animator.SetBool("isLand",true);

        player.jumpDirection = Vector3.zero;
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isLand",false);
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetAimCamera(false);
    }
}
