using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Vector3 directionPosition;
    private Vector3 targetDirection;

    public float maxSpeed  = 0f;

    public void SetDirection(Vector3 direction)
    {
        directionPosition = direction;
        rigidBody.AddForce(Vector3.up * 200f, ForceMode.Force);
    }
    
    private void Update() 
    {
        targetDirection = (directionPosition - transform.position).normalized;

        UseGravity();
        SpeedContoll();
    }

    public void UseGravity()
    {
        if (transform.position.y < directionPosition.y) return;

        rigidBody.AddForce(Vector3.down * 0.5f, ForceMode.Force);
    }

    public void SpeedContoll()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
        else
        {
            rigidBody.AddForce(targetDirection * 5f, ForceMode.Force);
        }
    }
}
