using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;

    Vector3 camAngle;
    Vector3 parentAngle;
    Vector2 mouseDelta;
    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        camAngle = transform.rotation.eulerAngles;
        parentAngle = transform.parent.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x,-1f,70f);
        }
        else
        {
            x = Mathf.Clamp(x,335f,361f);
        }

        transform.rotation = Quaternion.Euler(x, camAngle.y, camAngle.z);
        transform.parent.rotation = Quaternion.Euler(parentAngle.x,parentAngle.y + mouseDelta.x,parentAngle.z);

        // 회전값을 적용한 위치 계산
        Vector3 forwardDirection = transform.forward; // 카메라가 바라보는 방향
        Vector3 newPosition = transform.position + forwardDirection * 20f;

        // target 이동
        target.position = newPosition;
    }
}
