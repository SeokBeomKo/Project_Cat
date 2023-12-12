using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseCat : MonoBehaviour
{
    [Header("속도")]
    public float moveSpeed = 2.0f;

    [Header("추격 경로")]
    public Transform waypointsParent;

    [Header("시작 컷씬 포인트")]
    public Transform CutSceneStartPoint;

    public delegate void CatCutSceneHandle();
    public event CatCutSceneHandle OnCutSceneStart;
    public event CatCutSceneHandle OnCutSceneEnd;

    private bool checkJump = false;
    private int currentWaypointIndex = 0;
    private float jumpSpeed = 3.5f;
    private float rotationSpeed = 5.0f;
    private Animator animator;
    private Transform[] waypoints;
    private Transform previousWaypoint;

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
        CheckCutScene();

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

                if (yDifference > 0)
                {
                    if (!checkJump)
                    {
                        animator.SetTrigger("jump");
                        
                    }
                    checkJump = true;
                }
                else if(yDifference < 0)
                {
                    if(!checkJump)
                    {
                        animator.SetTrigger("drop");
                    }
                    checkJump = true;
                }
                else if (yDifference == 0)
                {
                    animator.SetTrigger("run");

                    if (checkJump)
                    {
                        checkJump = false;
                    }
                }
            }

            Vector3 targetPosition = new Vector3(currentWaypoint.position.x, currentWaypoint.position.y, currentWaypoint.position.z);
            if(checkJump)
            {
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * jumpSpeed);
            }
            else
            {
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * moveSpeed);
            }
        }
        else if (currentWaypointIndex == waypoints.Length)
        {
            animator.SetBool("endIdle", true);
        }
    }

    void CheckCutScene()
    {
        if (CutSceneStartPoint != null && currentWaypointIndex < waypoints.Length)
        {
            if (CutSceneStartPoint == waypoints[currentWaypointIndex])
            {
                Debug.Log("스타트 컷씬 이벤트 호출");
                OnCutSceneStart?.Invoke();
            }
            else if (currentWaypointIndex == waypoints.Length - 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("EndIdle"))
            {
                Debug.Log("엔드 컷씬 이벤트 호출");
                OnCutSceneEnd?.Invoke();
            }
        }
    }


}