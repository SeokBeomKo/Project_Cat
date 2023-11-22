using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [Header("플레이어 인풋 핸들")]
    [SerializeField] public InputHandler inputHandle;

    private void Start() 
    {
        inputHandle.OnPlayerRunInput += ChangeRunState;
        inputHandle.OnPlayerJumpInput += ChangeJumpState;
        inputHandle.OnPlayerDiveRollInput += ChangeRollState;
        inputHandle.OnPlayerAimSwitchInput += ChangeAimState;

        inputHandle.OnWeaponSwapInput += SwapWeapon;
    }

    void SwapWeapon(int number)
    {
        playerController.weaponCenter.SwapWeapon(number);
    }

    void ChangeAimState()
    {
        if (playerController.stateMachine.curState is PlayerMoveState moveState)
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.AIM_MOVE);
        }
        else
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.AIM);
        }
    }

    void ChangeRunState()
    {
        if (playerController.stateMachine.curState is PlayerAimState aimState)
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.AIM_MOVE);
        }
        else
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.MOVE);
        }
    }

    void ChangeJumpState()
    {
        if ((playerController.stateMachine.curState is PlayerJumpState jumpstate ||
            playerController.stateMachine.curState is PlayerFallState fallState) &&
            playerController.playerStats.GetDoubleCount() > 0)
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.DOUBLE);
        }
        else
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.JUMP);
        }
    }

    void ChangeRollState()
    {
        if (0 == playerController.playerStats.GetRollCount()) return;

        playerController.playerStats.UseRoll();

        if (playerController.stateMachine.curState is PlayerIdleState idleState)
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.BACKROLL);
        }
        else
        {
            playerController.stateMachine.ChangeStateInput(PlayerStateEnums.DIVEROLL);
        }
    }

    
}
