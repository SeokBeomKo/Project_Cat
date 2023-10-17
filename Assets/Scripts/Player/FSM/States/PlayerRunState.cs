using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerRunState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        player.animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        player.animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateMachine.ChangeState(PlayerStateEnums.IDLE);
            return;
        }
        if (Input.GetAxisRaw("Jump") == 1)
        {
            stateMachine.ChangeState(PlayerStateEnums.JUMP);
            return;
        }
        if (Input.GetAxisRaw("DiveRoll") == 1)
        {
            stateMachine.ChangeState(PlayerStateEnums.DIVEROLL);
            return;
        }

        player.Run();
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("isRun",true);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isRun",false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}