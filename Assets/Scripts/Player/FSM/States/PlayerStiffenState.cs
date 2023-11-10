using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStiffenState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.IDLE,
        PlayerStateEnums.MOVE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerStiffenState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")&&
            player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    stateMachine.ChangeStateLogic(PlayerStateEnums.MOVE);
                    return;
                }
                else
                {
                    stateMachine.ChangeStateLogic(PlayerStateEnums.IDLE);
                    return;
                }
            }
    }

    public void OnStateEnter()
    {
        // 플레이어의 움직임을 제한합니다.
        player.rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // 피격 애니메이션을 재생합니다.
        player.animator.SetTrigger("onHit");

        Debug.Log("경직");
    }

    public void OnStateExit()
    {
        // 플레이어의 움직임 제한을 해제합니다.
        player.rigid.constraints = RigidbodyConstraints.FreezeRotation;
    }
}