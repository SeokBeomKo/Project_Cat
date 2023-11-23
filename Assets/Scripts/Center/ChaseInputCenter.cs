using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseInputCenter : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [Header("플레이어 인풋 핸들")]
    [SerializeField] public InputHandler inputHandle;

    private void Start() 
    {
        inputHandle.OnPlayerRunInput += ChangeRunState;
    }

    private void OnEnable() 
    {
        EnterMaze();
    }

    public void EnterMaze()
    {
        playerController.transform.parent.LookAt(playerController.transform.parent.position + Vector3.forward);
        playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.CHASE_IDLE);
    }

    public void ChangeRunState()
    {
        playerController.stateMachine.ChangeStateInput(PlayerMovementStateEnums.CHASE_MOVE);
    }
}
