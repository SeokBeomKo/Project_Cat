using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseCat : MonoBehaviour
{
    private Transform[] waypoints;
    public Transform waypointsParent;
    private int currentWaypointIndex = 0;
    public float moveSpeed = 2.0f;
    public float jumpHeightThreshold = 0.5f;
    public float jumpDistance = 1.0f;
    public float rotationSpeed = 5.0f;

    private Animator animator;
    private Transform previousWaypoint;

    public Transform CutSceneStartPoint;
    public delegate void CatCutSceneHandle();
    public event CatCutSceneHandle OnCutSceneStart;

    private void Start()
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
        Check();

        if (currentWaypointIndex < waypoints.Length)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];

            Vector3 direction = (currentWaypoint.position - transform.parent.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);

            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            float yDifference = Mathf.Abs(previousWaypoint.position.y - currentWaypoint.position.y);

            if (yDifference > jumpHeightThreshold)
            {
                animator.SetTrigger("jump");
            }
            else if(yDifference == 0)
            {
                animator.SetTrigger("run");
            }

            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.parent.position, currentWaypoint.position) < 0.1f)
            {
                previousWaypoint = currentWaypoint;
                currentWaypointIndex++;
            }
        }
        else if (currentWaypointIndex == waypoints.Length)
        {
            animator.SetBool("endIdle", true);
        }
    }

    void Check()
    {
        if(currentWaypointIndex < waypoints.Length &&
            CutSceneStartPoint == waypoints[currentWaypointIndex])
        {
            Debug.Log("event Invoke");
            OnCutSceneStart?.Invoke();
        }
    }


}