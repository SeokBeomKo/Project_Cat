using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchChaseOperation : MonoBehaviour
{
    public GameObject SwitchButton;
    public GameObject Door;
    public float DoorMoveSpeed = 5.0f; // 버튼 내리는 속도

    private bool isSwitchOn;
    private bool isDoorMoving;

    private void Update()
    {
        if(isDoorMoving)
        {
            DoorMove();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isSwitchOn)
            {
                Debug.Log("Switch Off");
                isSwitchOn = false;
            }
            else
            {
                Debug.Log("Switch On");
                isSwitchOn = true;
            }
            isDoorMoving = true;

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("발사체"))
        {
            if (isSwitchOn)
            {
                isSwitchOn = false;
            }
            else
            {
                isSwitchOn = true;
            }

            SwitchButtonRotate();

            isDoorMoving = true;
        }
    }

    private void SwitchButtonRotate()
    {
        if (isSwitchOn)
        {
            SwitchButton.transform.Rotate(40, 0, 0);
        }
        else
        {
            SwitchButton.transform.Rotate(-40, 0, 0);
        }
    }

    private void DoorMove()
    {
        if(isSwitchOn)
        {
            Door.transform.Translate(Vector3.up * DoorMoveSpeed * Time.deltaTime);
            if(Door.transform.position.x > 2)
            {
                isDoorMoving = false;
            }
        }
        else
        {
            Door.transform.Translate(Vector3.down * DoorMoveSpeed * Time.deltaTime);
            if (Door.transform.position.x < 0.48)
            {
                isDoorMoving = false;
            }
        }
    }
}
