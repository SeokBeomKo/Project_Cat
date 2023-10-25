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
        rigid.velocity = new Vector3(moveDir.x * moveSpeed, rigid.velocity.y, moveDir.z * moveSpeed);
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
        rigid.velocity = new Vector3(rigid.velocity.x, jumpPower, rigid.velocity.z);
    }

    public void JumpMove()
    {
        rigid.velocity = new Vector3(jumpDir.x * moveSpeed, rigid.velocity.y, jumpDir.z * moveSpeed);
    }

    public void SetJumpDir()
    {
        jumpDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        jumpDir = transform.rotation * jumpDir; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
    }

    public bool CheckGrounded()
    {
        float raycastDistance = 0.1f; // 레이 캐스트 거리
        RaycastHit hit;

        // 발 아래로 레이 캐스트를 발사하여 땅에 닿았는지 체크
        if (Physics.Raycast(transform.position + new Vector3(0,0.05f,0), Vector3.down, out hit, raycastDistance))
        {
            // 레이캐스트 렌더링 (빨간색)
            Debug.DrawLine(transform.position + new Vector3(0,0.05f,0), transform.position + new Vector3(0,0.05f,0) + Vector3.down * raycastDistance, Color.red);

            // 땅에 닿았으면 true 반환
            return true;
        }

        // 레이캐스트 렌더링 (파란색)
        Debug.DrawLine(transform.position + new Vector3(0,0.05f,0), transform.position + new Vector3(0,0.05f,0) + Vector3.down * raycastDistance, Color.blue);

        // 땅에 닿지 않았으면 false 반환
        return false;
    }
}
