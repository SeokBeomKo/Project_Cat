using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackRollState :  IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.IDLE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerBackRollState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Backward Roll")&&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.IDLE);
            return;
        }
        Debug.Log(player.animator.GetCurrentAnimatorStateInfo(0).IsName("Backward Roll"));
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Backward Roll") && 
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.5f)
        {
            player.isRolled = true;
        }
        else
        {
            player.isRolled = false;
        }
    }

    public void OnStateEnter()
    {
        Rolling(true);
        ClearAimSetting();

        player.playerStats.UseRoll();

        player.RollInput();
    }

    public void OnStateExit()
    {
        Rolling(false);

        player.rigid.velocity = Vector3.zero;
    }

    public void Rolling(bool set)
    {
        player.Invaison(set);
        player.animator.SetBool("isDiveRoll",set);
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}
