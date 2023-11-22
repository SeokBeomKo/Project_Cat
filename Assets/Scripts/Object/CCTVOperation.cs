using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVOperation : MonoBehaviour
{
    public Material Screen;
    public GameObject Barrier;

    private bool isBarrierMove = false;
    private bool isPlayerCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.DisableKeyword("_EMISSION");

    }

    private void Update()
    {
        if (isBarrierMove)
        {
            MoveBarrier();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isBarrierMove && isPlayerCollision)
        {
            Debug.Log("Input F");
            isBarrierMove = true;
        }

    }

    private void MoveBarrier()
    {
        Vector3 barrierPosition = Barrier.transform.position;
        if (barrierPosition.y > 1.5)
        {
            Barrier.transform.position += new Vector3(0, -1f, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerCollision = true;
            Screen.EnableKeyword("_EMISSION");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerCollision = false;
    }
}
