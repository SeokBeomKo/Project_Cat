using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject barrier;
    
    private bool isBarrierClose = false;

    private IEnumerator MoveBarrierCoroutine()
    {
        while (!isBarrierClose)
        {
            Vector3 barrierPosition = barrier.transform.localPosition;
            if (barrierPosition.y > 0.14f)
            {
                barrier.transform.position += new Vector3(0, -0.1f, 0) * Time.deltaTime * 8f;
            }
            else
            {
                // Set isBarrierClose to true when the condition is met
                isBarrierClose = true;
            }

            // Wait for the next frame
            yield return null;
        }
    }
}
