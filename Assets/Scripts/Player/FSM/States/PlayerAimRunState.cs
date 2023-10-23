using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimRunState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.RUN,
        PlayerStateEnums.AIM,
    };

    public PlayerAimRunState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        player.animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        player.animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.AIM);
            return;
        }

        if (!Input.GetButton("Fire2"))
        {
            player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
            player.cameraController.SetAimCamera(false);
            stateMachine.ChangeStateLogic(PlayerStateEnums.RUN);
        }

        player.Run();
    }

    public void OnStateEnter()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 1);
        player.cameraController.SetAimCamera(true);
        player.animator.SetBool("isRun",true);
        player.curDoubleCount = player.maxDoubleCount;
    }

    public void OnStateExit()
    {
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);
        player.animator.SetBool("isRun",false);

        Debug.Log("a");
    }
}
