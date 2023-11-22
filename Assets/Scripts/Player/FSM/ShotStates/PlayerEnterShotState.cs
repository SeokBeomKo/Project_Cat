using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterShotState : IPlayerShotState
{
    public PlayerController player { get; set; }
    public PlayerShotStateMachine stateMachine { get; set; }

    public PlayerEnterShotState(PlayerShotStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        Debug.Log("Enter");
        stateMachine.ChangeState(PlayerShotStateEnums.EXCUTE);
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }
}
