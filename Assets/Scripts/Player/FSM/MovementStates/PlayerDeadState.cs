using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerDeadState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {

    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.rigid.constraints = RigidbodyConstraints.FreezeAll;
        player.animator.SetTrigger("onDead");
    }

    public void OnStateExit()
    {
        
    }

    public void ClearAimSetting()
    {
        player.shotstateMachine.ChangeState(PlayerShotStateEnums.NOTHING);
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}
