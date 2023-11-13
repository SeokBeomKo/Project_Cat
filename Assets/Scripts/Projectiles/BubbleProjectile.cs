using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Vector3 directionPosition;

    private void Start() 
    {
        rigidBody.AddForce(Vector3.up, ForceMode.Force);
    }

    public void SetDirection(Vector3 direction)
    {
        directionPosition = direction;
    }

    private void Update() 
    {
        rigidBody.velocity = directionPosition * 1f + (Vector3.down * 0.1f);
    }
}
