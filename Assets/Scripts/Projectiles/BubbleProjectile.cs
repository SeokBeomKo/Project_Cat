using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Vector3 directionPosition;

    public float maxSpeed  = 0f;

    public bool isBounce;

    private void Start() 
    {
        // 수직 방향으로 힘을 가함
        
    }

    public void SetDirection(Vector3 direction)
    {
        directionPosition = direction;
        rigidBody.AddForce(Vector3.up * 200 + directionPosition * 100, ForceMode.Force);
    }

    public void UseGravity()
    {
        rigidBody.AddForce(Vector3.down * 0.5f, ForceMode.Force);
    }

    private void Update() 
    {
        UseGravity();
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
        else
        {
            rigidBody.AddForce(directionPosition * 5f, ForceMode.Force);
        }
    }
}
