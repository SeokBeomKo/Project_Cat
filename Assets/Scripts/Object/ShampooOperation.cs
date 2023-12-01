using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShampooOperation : MonoBehaviour
{
    private Rigidbody ShampooRigidbody;
    public float collapseDelay = 0.5f; // 딜레이 시간 (초)
    private bool isFallen = false; // 이미 쓰러진 상태인지 여부를 나타내는 변수
    private bool FieldCreate = false;
    public float rotationSpeed = 5f; // 회전 속도
    

    private void Start()
    {
        ShampooRigidbody = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (!isFallen)
        {
            float zRotationInDegrees = transform.rotation.eulerAngles.z;

            Debug.Log(zRotationInDegrees);

            if (zRotationInDegrees % 90 == 0)
            {
                ShampooRigidbody.isKinematic = true;
                FieldCreate = true;
                isFallen = true;
            }
        }

        if(FieldCreate)
        {

        }

    }

}
