using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCleanerSwitch : MonoBehaviour
{
    public RobotCleanerMovement RobotCleaner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 감지
        {
            if (RobotCleaner != null)
            {
                if (transform.name == "20percent")
                {
                    RobotCleaner.StartMoving();
                }
                else
                {
                    RobotCleaner.StopMoving();
                }
            }
        }
    }
}
