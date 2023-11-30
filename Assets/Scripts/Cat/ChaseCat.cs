using UnityEngine;
using UnityEngine.AI;

public class ChaseCat : MonoBehaviour
{
    public Animator animator;
    private string targetTag = "ChaseRoad";
    public float jumpHeightThreshold = 0.5f; // Y 축 차이의 임계값

    private Transform[] targets;
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private bool allTargetsReached = false;

    void Start()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        targets = new Transform[targetObjects.Length];

        for (int i = 0; i < targetObjects.Length; i++)
        {
            targets[i] = targetObjects[i].transform;
        }

        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInParent<Animator>();

        if (targets.Length > 0)
        {
            SetNextDestination();
        }
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f && !allTargetsReached)
        {
            SetNextDestination();
        }

        if (allTargetsReached)
        {
            Debug.Log("모든 목표 지점에 도착했습니다!");
        }
    }

    void SetNextDestination()
    {
        if (currentTargetIndex < targets.Length)
        {
            if (agent.destination != targets[currentTargetIndex].position)
            {
                agent.SetDestination(targets[currentTargetIndex].position);
                currentTargetIndex++;

                if (currentTargetIndex < targets.Length)
                {
                    // 목적지 간의 Y 축 차이 계산
                    float yDifference = Mathf.Abs(transform.position.y - targets[currentTargetIndex].position.y);

                    // Y 축 차이가 특정 높이 이상이면 점프
                    if (yDifference > jumpHeightThreshold)
                    {
                        Jump();
                    }

                }
                else
                {
                    currentTargetIndex++;
                    SetNextDestination();
                }
            }
            else
            {
                allTargetsReached = true;
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            // 체크: currentTargetIndex가 배열의 범위 내에 있는지 확인
            if (currentTargetIndex < targets.Length)
            {
                // 목적지 간의 Y 축 차이 계산
                float yDifference = Mathf.Abs(transform.position.y - targets[currentTargetIndex].position.y);

                // Y 축 차이가 특정 높이 이상이면 점프
                if (yDifference > jumpHeightThreshold)
                {
                    Jump();
                }

                // 이후에 currentTargetIndex 증가
                currentTargetIndex++;
            }
            else
            {
                // 모든 목표 도착 시에 처리할 내용 추가
                allTargetsReached = true;
            }
        }
    }*/

    void Jump()
    {
        animator.SetTrigger("jump");
    }
}
