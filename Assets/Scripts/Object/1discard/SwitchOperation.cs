using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwitchOperation : MonoBehaviour
{
    public GameObject button; // 버튼 오브젝트
    public float downSpeed = 1.0f; // 버튼 내리는 속도
    public float targetY = 0.3f; // 버튼이 도달할 Y 좌표
    public float threshold = 1.0f; // 힘의 임계값

    public Color32 activatedColor = new Color32(255, 102, 102, 255); // 활성화된 상태의 색

    private Rigidbody buttonRigidbody;
    private float collisionForce;

    private bool isActivated = false; // 버튼 활성화 상태 여부
    private Vector3 initialPosition; // 초기 위치 저장
    private bool isButtonMoving = false; // 버튼 움직이는 중 여부
    
    private void Start()
    {
        initialPosition = button.transform.position;
    }

    private void Update()
    {
        if (isActivated)
        {
            button.GetComponent<Renderer>().material.color = activatedColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.CompareTag(""))
        {

        }
        StartCoroutine(PressButton());
        // 충돌한 객체의 힘을 가져오고, 그 힘이 threshold보다 큰지 확인
        //collisionForce = collision.relativeVelocity.magnitude;// + (Physics.gravity.magnitude * collision.rigidbody.mass);
        //if (collisionForce >= threshold && !isButtonMoving)
        //{
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌한 객체가 사라졌을 때
        if (!isButtonMoving)
        {
            StartCoroutine(ReleaseButton());
        }
    }

    private IEnumerator PressButton()
    {
        isButtonMoving = true;

        while (!isActivated && button.transform.position.y > targetY)
        {
            button.transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
            yield return null;
        }

        if (!isActivated)
        {
            isActivated = true;
        }

        isButtonMoving = false;
    }

    private IEnumerator ReleaseButton()
    {
        isButtonMoving = true;

        while (button.transform.position.y < initialPosition.y)
        {
            button.transform.Translate(Vector3.up * downSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * downSpeed * Time.deltaTime);

            yield return null;
        }

        isButtonMoving = false;
    }

    public bool GetSwitchState()
    {
        return isActivated;
    }

    //public void SetSwitchPress(bool press)
    //{
    //    this.press = press;
    //    if (press)
    //    {
    //        collisionForce = 10;
    //    }
    //}

}
