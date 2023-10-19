using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("애니메이터")]
    [SerializeField]    public Animator             animator;

    [Header("리지드바디")]
    [SerializeField]    public Rigidbody            rigid;

    [Header("유한 상태 기계")]
    [SerializeField]    public PlayerStateMachine   stateMachine;
    [Header("모델")]
    [SerializeField]    public Transform            model;


    [Header("수치 값")]
    [SerializeField]    public float                moveSpeed;
    [SerializeField]    public float                jumpPower;
    [SerializeField]    public float                diveSpeed;

    private void FixedUpdate()
    {
        if (null != stateMachine.curState)
        {
            stateMachine.curState.Execute();
        }
    }

    Vector3 moveDir;
    public void Run()
    {
        moveDir = new Vector3(animator.GetFloat("Horizontal"),0,animator.GetFloat("Vertical"));

        moveDir = transform.rotation * moveDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
        rigid.MovePosition(rigid.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void DiveRoll(Vector3 _diveDir)
    {
        _diveDir = transform.rotation * _diveDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
        rigid.MovePosition(rigid.position + _diveDir * diveSpeed * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
