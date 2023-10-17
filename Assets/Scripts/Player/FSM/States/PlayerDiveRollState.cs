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
            stateMachine.ChangeState(PlayerStateEnums.Idle);
            return;
        }

        if (diveDir != Vector3.zero) 
        {
            Debug.Log("ROTATIONING");
            player.model.rotation = Quaternion.LookRotation(diveDir); // 새로운 방향으로 회전
        }
    }

    public void OnStateEnter()
    {
        origDir = player.model.rotation; // 원래 회전값 저장
        Debug.Log("ORIGINAL : " + origDir);
        // diveDir = new Vector3(player.animator.GetFloat("Horizontal"),0,player.animator.GetFloat("Vertical"));
        diveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Debug.Log("DIVEROLL : " + diveDir);

        player.animator.SetBool("isDiveRoll",true);
    }

    public void OnStateExit()
    {
        player.model.rotation = Quaternion.LookRotation(Vector3.forward); // 원래의 회전값으로 복구
        Debug.Log("REROTATION : " + player.model.rotation);

        player.animator.SetBool("isDiveRoll",false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}