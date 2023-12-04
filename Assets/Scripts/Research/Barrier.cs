using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private bool isBarrierClose = false;

    public delegate void BarrierHandle();
    public event BarrierHandle OnBarrier;

    public IEnumerator MoveBarrierCoroutine()
    {
        while (!isBarrierClose)
        {
            Vector3 barrierPosition = transform.localPosition;
            if (barrierPosition.y > 0.14f)
            {
                transform.position += new Vector3(0, -0.1f, 0) * Time.deltaTime * 8f;
            }
            else
            {
                // Set isBarrierClose to true when the condition is met
                isBarrierClose = true;

                Invoke("SuccessCloseBarrier", 2f);
            }

            // Wait for the next frame
            yield return null;
        }
    }

    private void SuccessCloseBarrier()
    {
        OnBarrier?.Invoke();
    }
}
