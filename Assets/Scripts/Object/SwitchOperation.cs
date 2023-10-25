using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwitchOperation : MonoBehaviour
{
    [Header("힘이 가해졌을 때 색이 변할 Mesh")]
    public MeshRenderer Mesh;
    Material mat;

    [Header("힘이 가해질 대상")]
    public GameObject Button;

    private bool press = false;
    private bool up = false;
    private bool isActivated = false;

    private Vector3 buttonOriginPosition;

    [Header("힘을 감지할 임계치")]
    public float ForceThreshold = 10.0f;
    float collisionForce;

    [Header("대상 이동 속도")]
    public float Speed = 5.0f; // 이동 속도 설정

    Collision collisionObject;

    [Header("스위치 작동시 움직일 대상")]
    public GameObject Door;
    public float DoorSpeed = 2;

    private bool isClosed = false;

    private void Start()
    {
        buttonOriginPosition = Button.transform.position;
        mat = Mesh.material;
    }

    void Update()
    {
        if (!isActivated && press)
        {
            PressSwitch();
        }

        if (!isActivated && up)
        {
            UpSwitch();
        }

        if(isActivated && !isClosed)
        {
            CloseDoor();
        }

        if(isActivated)
        {
            mat.color = new Color32(255, 102, 102, 255);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionObject = collision;
        Rigidbody collisionRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        float collisionMass = collisionRigidbody.mass;
        float gravity = 9.8f;

        collisionForce = collision.relativeVelocity.magnitude + collisionMass * gravity;

        if ((collisionForce >= ForceThreshold) && !isActivated)
        {
            press = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        up = true;
        press = false;
        mat.color = new Color32(255, 0, 0, 255);
        collisionObject = null;
    }

    private void CloseDoor()
    {
        Door.transform.Translate(Vector3.down * DoorSpeed * Time.deltaTime);
        if (Door.transform.position.y < 3)
        {
            isClosed = true;
        }
    }

    private void PressSwitch()
    {
        Button.transform.Translate(Vector3.down * collisionForce * Time.deltaTime);
        collisionObject.transform.Translate(Vector3.down * collisionForce * Time.deltaTime);

        if (Button.transform.position.y < 0.3)
        {
            Button.transform.Translate(Button.transform.position.x, 0.4f, Button.transform.position.z);
            press = false;
            isActivated = true;
        }
        transform.position = Button.transform.position;

        mat.color = new Color32(255, 102, 102, 255);
    }

    private void UpSwitch()
    {
        if (Button.transform.position.y >= buttonOriginPosition.y)
        {
            up = false;
            transform.position = Button.transform.position;
        }
        else
        {
            Button.transform.Translate(Vector3.up * Speed * Time.deltaTime);
            if (collisionObject != null)
            {

                collisionObject.transform.Translate(Vector3.up * Speed * Time.deltaTime);
            }
        }
    }

}
