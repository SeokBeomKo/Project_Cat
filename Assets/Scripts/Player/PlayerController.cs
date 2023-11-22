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
    [SerializeField]    public PlayerStateMachine       stateMachine;
    [SerializeField]    public PlayerShotStateMachine   shotstateMachine;

    [Header("모델")]
    [SerializeField]    public Transform            model;

    [Header("스탯")]
    [SerializeField]    public PlayerStats          playerStats;

    [Header("무기")]
    [SerializeField]    public WeaponCenter         weaponCenter;

    [Header("체크")]
    [HideInInspector]   public bool                 isGrounded;
    [HideInInspector]   public bool                 isRolled;

    [Header("경사 각도")]
    private RaycastHit slopeHit;
    public float maxSlopeAngle;
    public bool exitingSlope = false;

    private void Update()
    {
        if (null != stateMachine.curState)
        {
            stateMachine.curState.Execute();
        }
    }

    private void FixedUpdate() 
    {
        isGrounded = CheckGrounded();
        SpeedControl();
        MoveRogic();
    }    


    private void SpeedControl()
    {
        if (OnSlope()&& !exitingSlope)
        {
            if (rigid.velocity.magnitude > playerStats.moveSpeed)
                rigid.velocity = rigid.velocity.normalized * playerStats.moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);

            if (flatVel.magnitude > playerStats.moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * playerStats.moveSpeed;
                rigid.velocity = new Vector3(limitedVel.x, rigid.velocity.y, limitedVel.z);
            }
        }
    }

    public Vector3 moveDirection;
    public Vector3 jumpDirection;
    public Vector3 rollDirection;

    public void AimSwitch()
    {
        cameraController.SwitchCamera();
    }

    public void MoveRogic()
    {
        if (OnSlope() && !exitingSlope)
        {
            rigid.AddForce(GetSlopeMoveDirection() * playerStats.moveSpeed * 20f, ForceMode.Force);

            if (rigid.velocity.y > 0)
                rigid.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        else if (isGrounded)
        {
            rigid.AddForce(moveDirection.normalized * playerStats.moveSpeed * 5f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, jumpDirection.normalized, out hit, 0.1f)) return;

            if (jumpDirection == Vector3.zero)
            {
                rigid.velocity = new Vector3(0,rigid.velocity.y,0);
            }
            else
            {
                rigid.AddForce(jumpDirection.normalized * playerStats.moveSpeed * 5f, ForceMode.Force);
            }
        }

        if (isRolled)
        {
            rigid.AddForce(rollDirection.normalized * playerStats.rollSpeed * 10f, ForceMode.Force);
        }

        rigid.useGravity = !OnSlope();
    }

    public void MoveInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveDirection = transform.rotation * moveDirection; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
    }

    public void JumpInput()
    {
        jumpDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

        jumpDirection = transform.rotation * jumpDirection; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
    }

    public void ResetJumpInput()
    {
        jumpDirection = Vector3.zero;
    }

    public void RollInput()
    {
        rollDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        if (rollDirection == Vector3.zero)   rollDirection = Vector3.back;
        rollDirection = transform.rotation * rollDirection; // 오브젝트의 회전을 적용하여 로컬 좌표계로 변환
    }

    public void Jump()
    {
        exitingSlope = true;
        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        
        rigid.AddForce(transform.up * playerStats.jumpForce, ForceMode.Impulse);
    }

    public void DoubleJump()
    {
        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        
        rigid.AddForce(transform.up * playerStats.jumpForce * 1.5f, ForceMode.Impulse);
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    float raycastDistance = 0.1f; // Raycast distance
    float colliderRadius = 0.25f; // Capsule collider radius
    public bool CheckGrounded()
    {
        Vector3[] origins = new Vector3[4];
        origins[0] = transform.position + new Vector3(-colliderRadius / 2f, 0.05f, 0); // Left
        origins[1] = transform.position + new Vector3(colliderRadius / 2f, 0.05f, 0); // Right
        origins[2] = transform.position + new Vector3(0 , 0.05f , -colliderRadius /2 ); // Front
        origins[3] = transform.position + new Vector3(0 , 0.05f , colliderRadius /2 ); // Back

        foreach (Vector3 origin in origins)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin , Vector3.down , out hit , raycastDistance))
            {
                return true;
            }
        }
        return false;
    }
}
