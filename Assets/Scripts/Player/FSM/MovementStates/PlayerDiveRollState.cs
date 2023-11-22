using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiveRollState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.MOVE,
        PlayerMovementStateEnums.IDLE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerDiveRollState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    Vector3 diveDir;
    Quaternion origDir; // 원래 회전값을 저장할 Quaternion 타입의 변수
    public void Execute()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Dive Roll")&&
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

        if (diveDir != Vector3.zero) 
        {
            player.model.localRotation = Quaternion.LookRotation(diveDir); // 새로운 방향으로 회전
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Dive Roll") &&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.7f)
        {
            player.isRolled = true;
        }
        else
        {
            player.isRolled = false;
            player.rigid.velocity = Vector3.zero;
        }
    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.playerStats.UseRoll();

        origDir = player.model.localRotation; // 원래 회전값 저장
        diveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        
        if (diveDir == Vector3.zero)   diveDir = Vector3.back;

        player.RollInput();

        player.animator.SetBool("isDiveRoll",true);
    }

    public void OnStateExit()
    {
        player.isRolled = false;
        player.model.localRotation = origDir;

        player.animator.SetBool("isDiveRoll",false);

        player.rigid.velocity = Vector3.zero;
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}