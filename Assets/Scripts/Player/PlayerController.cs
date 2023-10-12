using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("애니메이터")]
    [SerializeField]    public Animator             playerAnimator;
    
    [Header("유한 상태 기계")]
    [SerializeField]    public PlayerStateMachine   stateMachine;

    private void Update()
    {
        if (null != stateMachine.curState)
        {
            stateMachine.curState.Execute();
        }
    }
}
