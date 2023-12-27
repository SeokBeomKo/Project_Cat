using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCat : MonoBehaviour
{
    [Header("추격 경로")]
    public Transform waypointsParent;

    private int currentWaypointIndex = 0;
    private float rotationSpeed = 5.0f;
    [Header("속도")]
    public float moveSpeed = 2.0f;
    private Animator animator;
    private Transform[] waypoints;
    private Transform previousWaypoint;

    void Start()
    {
        animator = GetComponentInParent<Animator>();

        int childCount = waypointsParent.childCount;
        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
        previousWaypoint = waypoints[0];
    }

    void Update()
    {
        MoveCat();
    }

    private void MoveCat()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];

            Vector3 direction = (currentWaypoint.position - transform.parent.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Vector3.Distance(transform.parent.position, currentWaypoint.position) <= 0f)
            {
                previousWaypoint = currentWaypoint;
                currentWaypointIndex++;
            }
            else
            {
                float yDifference = previousWaypoint.position.y - currentWaypoint.position.y;

                animator.SetTrigger("run");
            }

            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }
}
