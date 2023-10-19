using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaitForSecondsPool;

public class PlayerJumpState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.DOUBLE,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public bool isJumpStarted = false;

    public PlayerJumpState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (!isJumpStarted) return;

        Debug.Log(player.rigid.velocity);
        if (player.rigid.velocity.y <= 0.1f)
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.FALL);
            return;
        }
    }


    public void OnStateEnter()
    {
        player.animator.SetBool("isJump", true);
        player.rigid.AddForce(Vector3.up * player.jumpPower, ForceMode.Impulse);
        player.StartCoroutine(JumpStart());
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isJump", false);
        isJumpStarted = false;
    }

    private IEnumerator JumpStart()
    {
        yield return WaitForSecondsPool.WaitForSecondsPool.waitForFixedUpdate;
        
        isJumpStarted = true;
    }
}