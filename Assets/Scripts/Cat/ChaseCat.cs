using UnityEngine;
using UnityEngine.AI;

/*public class ChaseCat : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float moveSpeed = 2.0f;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];

            Vector3 direction = (currentWaypoint.position - transform.parent.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.parent.rotation = targetRotation;

            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.parent.position, currentWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}*/

public class ChaseCat : MonoBehaviour
{
    private Transform[] waypoints;
    public Transform waypointsParent;
    private int currentWaypointIndex = 0;
    public float moveSpeed = 2.0f;
    public float jumpHeightThreshold = 0.5f;
    public float jumpDistance = 1.0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

        int childCount = waypointsParent.childCount;

        waypoints = new Transform[childCount];

        for(int i =0;i<childCount;i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
    }

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];

            Vector3 direction = (currentWaypoint.position - transform.parent.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.parent.rotation = targetRotation;

            float yDifference = Mathf.Abs(transform.parent.position.y - currentWaypoint.position.y);

            if (yDifference > jumpHeightThreshold)
            {
                Jump();
            }

            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.parent.position, currentWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }

    void Jump()
    {
        animator.SetTrigger("jump");
    }
}