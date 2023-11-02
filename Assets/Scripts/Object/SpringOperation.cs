using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringOperation : MonoBehaviour
{
    public float compressionForce = 1000f; // 스프링을 누를 때 가해질 힘
    public float releaseForce = 500f; // 스프링을 해제할 때 가해질 힘
    public Transform player; // 플레이어의 위치를 가리키는 트랜스폼
    public float jumpForce = 30f; // 점프력
    public Rigidbody springRigidbody; //= GetComponent<Rigidbody>();

    private bool isCompressed = false; // 스프링이 압축된 상태인지 여부

    public float minScale = 0.5f; // 최소 크기
    public float maxScale = 1.0f; // 최대 크기
    public float scaleSpeed = 1.0f; // 크기 조절 속도

    private Vector3 initialScale; // 초기 크기
    private bool shrinking = true; // 작아지는 중인지 여부

    private bool isCollision = false;
    private bool isCompressing = false;

    private void Start()
    {
        initialScale = springRigidbody.transform.localScale; // 초기 크기 저장
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 스프링과 충돌하면 스프링을 누립니다.
            isCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
    }

    private void Update()
    {
        if(isCollision)
        {
            Jump();
            isCompressing = true;
        }

        if(isCompressing)
        {
            CompressSpring();
        }

        if (isCompressed)// && player != null)
        {
            shrinking = false;
        }

    }

    private void CompressSpring()
    {
        if (shrinking)
        {
            // 작아지는 중
            Vector3 newScale = springRigidbody.transform.localScale - Vector3.up * scaleSpeed * 0.01f;
            newScale.y = Mathf.Max(newScale.y, minScale); // 최소 크기 제한
            springRigidbody.transform.localScale = newScale;

            // 최소 크기에 도달하면 반전
            if (newScale.y <= minScale)
            {
                isCompressed = true;
            }
        }
        else
        {
            // 커지는 중
            Vector3 newScale = springRigidbody.transform.localScale + Vector3.up * scaleSpeed * 0.01f;
            newScale.y = Mathf.Min(newScale.y, maxScale); // 최대 크기 제한
            springRigidbody.transform.localScale = newScale;

            // 최대 크기에 도달하면 반전
            if (newScale.y >= maxScale)
            {
                shrinking = true;
                isCompressed = false;
                isCompressing = false;
            }
        }

       
    }

    private void Jump()
    {
        // 스프링을 누르면 플레이어를 위로 점프시킵니다.
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * jumpForce);
        //isCompressed = false; // 스프링을 해제합니다.
    }
}