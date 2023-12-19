using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantChaseOperation : MonoBehaviour
{
    public GameObject Wall;

    private float rotationSpeed = 200f;
    private float moveSpeed = 0.7f;

    private bool isRotation = false;
    private bool isFalling = false;
    private bool isRolling = false;

    private Vector3 forceDirection;

    [Header("데이터")]
    public SpeedData data;

    private float forceMagnitude;

    private Rigidbody plantRigidbody;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        forceMagnitude = data.speed;
    }

    private void Start()
    {
        plantRigidbody = GetComponent<Rigidbody>();

        plantRigidbody.useGravity = false;
        plantRigidbody.isKinematic = true;

    }
    private void OnCollisionEnter(Collision collision)
    {
        string otherTag = collision.transform.tag;

        if (otherTag.Contains("Parts"))
        {
            isRotation = true;
        }
        if (collision.gameObject.CompareTag("ChaseRoad"))
        {
            isFalling = false;
            isRolling = true;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(5);
        }
    }

    private void Update()
    {
        if(isRotation)
        {
            PlantRotate();
        }

        if(isFalling)
        {
            PlantFall();
        }

        if (isRolling)
        {
            PlantRoll();
        }
    }

    
    private void PlantRotate()
    {
        float zRotation = transform.eulerAngles.z + rotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
        if (zRotation > 90)
        {
            plantRigidbody.useGravity = true;
            plantRigidbody.isKinematic = false;
            isRotation = false;
            isFalling = true;
        }

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void PlantFall()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void PlantRoll()
    {

        forceDirection = Wall.transform.position - transform.position;
        forceDirection.y = 0;

        plantRigidbody.AddForce(forceDirection * forceMagnitude);
    }
}
