using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    [Header("돌진 시 밀어내는 강도")]

    public float playerSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb = other.GetComponentInParent<Rigidbody>();

            Vector3 pushDirection = - other.transform.parent.position + transform.parent.position;
            pushDirection.y = 0f;
            rb.AddForce(Vector3.back * playerSpeed, ForceMode.Impulse);
        }
    }

}
