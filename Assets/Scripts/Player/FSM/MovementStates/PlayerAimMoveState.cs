using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimMoveState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.MOVE,
        PlayerMovementStateEnums.AIM,

        PlayerMovementStateEnums.AIM_MOVE_SHOOT,
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
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.AIM);
            return;
        }

        if (!Input.GetButton("Fire2"))
        {
            player.cameraController.SetAimCamera(false);
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.MOVE);
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.shotstateMachine.ChangeState(PlayerShotStateEnums.ENTER);
        }

        player.MoveInput();
    }

    float originSpeed;
    public void OnStateEnter()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 1);
        player.cameraController.SetAimCamera(true);
        player.playerStats.FillDoubleCount();

        originSpeed = player.playerStats.moveSpeed;
        player.playerStats.moveSpeed *= 0.5f; 
    }

    public void OnStateExit()
    {
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);

        player.moveDirection = Vector3.zero;
        player.rigid.velocity = Vector3.zero;

        player.playerStats.moveSpeed = originSpeed;
    }
}
