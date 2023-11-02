using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAndTrack : MonoBehaviour
{
    public float hoverTime;
    public float trackingDistance;

    private float hoverEndTime;

    private void Start()
    {
        hoverEndTime = Time.time + hoverTime;
    }

    private void Update()
    {
        // 부유 시간이 끝나면 가장 가까운 Virus 또는 Cat을 추적합니다.
        if (Time.time > hoverEndTime)
        {
            GameObject closestTarget = FindClosestTarget();
            if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.transform.position) <= trackingDistance)
            {
                transform.LookAt(closestTarget.transform);
                GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<BubbleTest>().projectileForce);
            }
        }
    }

    private GameObject FindClosestTarget()
    {
        float closestDistance = float.MaxValue; // 가장 가까운 거리를 저장할 변수를 초기화합니다.
        GameObject closestTarget = null; // 가장 가까운 대상을 저장할 변수를 초기화합니다.

        // 현재 위치에서 일정 범위 내의 모든 콜라이더를 찾습니다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, trackingDistance);

        foreach (var collider in colliders)
        {
            // 콜라이더의 태그가 "Cat" 또는 "Virus"인 경우에만 처리합니다.
            if (collider.tag == "Cat" || collider.tag == "Virus")
            {
                // 현재 콜라이더와의 거리를 계산합니다.
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                // 현재 콜라이더가 지금까지 찾은 가장 가까운 대상보다 가까운 경우, 이를 저장합니다.
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider.gameObject;
                }
            }
        }

        // 가장 가까운 대상을 반환합니다.
        return closestTarget;
    }
}
