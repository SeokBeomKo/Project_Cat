using UnityEngine;
using UnityEngine.AI;

public class ChaseCat : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;

    private string targetTag = "ChaseRoad";

    private Transform[] targets;
    private int currentTargetIndex = 0;
    private NavMeshAgent agent;
    private bool allTargetsReached = false;

    void Start()
    {
        // ChaseRoad 태그로 된 모든 목적지를 찾아 배열에 할당
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        targets = new Transform[targetObjects.Length];

        for (int i = 0; i < targetObjects.Length; i++)
        {
            targets[i] = targetObjects[i].transform;
        }

        agent = transform.parent.GetComponent<NavMeshAgent>();

        if (targets.Length > 0)
        {
            SetNextDestination();
        }
        else
        {
            Debug.LogError("No targets with tag '" + targetTag + "' found!");
        }
    }

    void Update()
    {
        // 목적지에 도착하면 다음 목적지로 이동
        if (agent.remainingDistance < 0.5f && !allTargetsReached)
        {
            SetNextDestination();
        }

        // 모든 목적지를 다 도착했을 때의 동작
        if (allTargetsReached)
        {
            Debug.Log("All targets reached!");
            // 여기에 추가적인 동작을 추가할 수 있습니다.
        }
    }

    void SetNextDestination()
    {
        // 현재 목적지를 배열에서 선택하고 NavMeshAgent에 설정
        if (currentTargetIndex < targets.Length)
        {
            agent.SetDestination(targets[currentTargetIndex].position);
            currentTargetIndex++;
        }
        else
        {
            allTargetsReached = true;
        }
    }
}