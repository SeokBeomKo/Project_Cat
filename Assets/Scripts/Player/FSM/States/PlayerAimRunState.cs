using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimMoveState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.MOVE,
        PlayerStateEnums.AIM,
    };

    public PlayerAimMoveState(PlayerStateMachine _stateMachine)
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
            stateMachine.ChangeStateLogic(PlayerStateEnums.MOVE);
        }

        player.MoveInput();
    }

    float originSpeed;
    public void OnStateEnter()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 1);
        player.cameraController.SetAimCamera(true);
        player.stats.FillDoubleCount();

        originSpeed = player.stats.moveSpeed;
        player.stats.moveSpeed *= 0.5f; 
    }

    public void OnStateExit()
    {
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);

        player.moveDirection = Vector3.zero;
        player.rigid.velocity = Vector3.zero;

        player.stats.moveSpeed = originSpeed;
    }
}
