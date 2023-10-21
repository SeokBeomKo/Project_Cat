using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
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
    }
}
