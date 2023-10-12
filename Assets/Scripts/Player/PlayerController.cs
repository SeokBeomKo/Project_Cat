using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("애니메이터")]
    [SerializeField]    public Animator             playerAnimator;

    [Header("리지드바디")]
    [SerializeField]    public Rigidbody            rigid;

    [Header("유한 상태 기계")]
    [SerializeField]    public PlayerStateMachine   stateMachine;


    [Header("수치 값")]
    [SerializeField]    public float                moveSpeed;

    private void FixedUpdate()
    {
        if (null != stateMachine.curState)
        {
            stateMachine.curState.Execute();
        }
    }


    public void Run(Vector3 _movedir)
    {
        rigid.MovePosition(transform.position + _movedir * moveSpeed * Time.fixedDeltaTime);
    }
}
