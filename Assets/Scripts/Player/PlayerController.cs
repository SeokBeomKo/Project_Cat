using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("카메라 컨트롤러")]
    [SerializeField]    public ShooterCameraController  cameraController;
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
    [SerializeField]    public int                  maxDoubleCount;
    [SerializeField]    public int                  curDoubleCount;

    private void Awake() 
    {
        curDoubleCount = maxDoubleCount;
    }

    private void Update()
    {
        if (null != stateMachine.curState)
        {
            stateMachine.curState.Execute();
        }
    }

    Vector3 moveDir;
    Vector3 jumpDir;

    public void AimSwitch()
    {
        cameraController.SwitchCamera();
    }

    public void Run()
    {
        moveDir = new Vector3(animator.GetFloat("Horizontal"), 0, animator.GetFloat("Vertical"));

        moveDir = transform.rotation * moveDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
        rigid.velocity = moveDir * moveSpeed;
    }

    public void DiveRoll(Vector3 _diveDir)
    {
        _diveDir = transform.rotation * _diveDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
        rigid.velocity = _diveDir * diveSpeed;
    }

    public void BackRoll(Vector3 _diveDir)
    {
        rigid.velocity = transform.rotation * Vector3.back * diveSpeed;
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    public void JumpMove()
    {
        // rigid.MovePosition(rigid.position + jumpDir * moveSpeed * Time.fixedDeltaTime);
        rigid.velocity = jumpDir * moveSpeed;
    }

    public void SetJumpDir()
    {
        jumpDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        jumpDir = transform.rotation * jumpDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
    }
}
