using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiveRollState : IPlayerState
{
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
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Running Dive Roll") &&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            stateMachine.ChangeState(PlayerStateEnums.IDLE);
            return;
        }

        if (diveDir != Vector3.zero) 
        {
            player.model.localRotation = Quaternion.LookRotation(diveDir); // 새로운 방향으로 회전
        }

        player.DiveRoll(diveDir);
    }

    public void OnStateEnter()
    {
        origDir = player.model.localRotation; // 원래 회전값 저장
        diveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        player.animator.SetBool("isDiveRoll",true);
    }

    public void OnStateExit()
    {
        player.model.localRotation = origDir;

        player.animator.SetBool("isDiveRoll",false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}