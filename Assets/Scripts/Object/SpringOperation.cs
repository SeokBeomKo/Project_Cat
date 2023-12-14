using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringOperation : MonoBehaviour
{
    [Header("데이터")]
    public SpeedData data;
    private float jumpForce;// 점프력

    private float minScale = 0.2f; // 최소 크기
    private float maxScale = 0.5f; // 최대 크기
    private float scaleSpeed = 1.0f; // 크기 조절 속도

    public Rigidbody springRigidbody;

    private Rigidbody playerRigidbody;

    private bool isCompressed = false; // 스프링이 압축된 상태인지 여부



    private bool shrinking = true; // 작아지는 중인지 여부
    private bool isCollision = false;
    private bool isCompressing = false;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        jumpForce = data.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRigidbody = other.gameObject.GetComponentInParent<Rigidbody>();
          
            isCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
    }

    private void Update()
    { 
        if (isCollision)
        {
            Jump();
            isCompressing = true;
        }

        if (isCompressing)
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
        playerRigidbody.AddForce(Vector3.up * jumpForce);
    }

}