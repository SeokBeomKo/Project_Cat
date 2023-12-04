using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitShotState : IPlayerShotState
{
    public PlayerController player { get; set; }
    public PlayerShotStateMachine stateMachine { get; set; }

    public PlayerExitShotState(PlayerShotStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        stateMachine.ChangeState(PlayerShotStateEnums.NOTHING);
    }

    public void OnStateEnter()
    {
        player.weaponStrategy.ExitShoot();
    }

    public void OnStateExit()
    {
        if (player.stateMachine.curState is PlayerAimState aimstatee ||
        player.stateMachine.curState is PlayerAimMoveState aimmovestate) return;
        else
        {
            player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
            player.SetRigWeight(0);
        }
    }
}
