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
        inputHandle.OnPlayerDiveRollInput += ChangeDiveRollState;
    }

    void ChangeRunState()
    {
        playerController.stateMachine.ChangeStateInput(PlayerStateEnums.RUN);
    }

    void ChangeJumpState()
    {
        playerController.stateMachine.ChangeStateInput(PlayerStateEnums.JUMP);
    }

    void ChangeDiveRollState()
    {
        playerController.stateMachine.ChangeStateInput(PlayerStateEnums.DIVEROLL);
    }

    
}
