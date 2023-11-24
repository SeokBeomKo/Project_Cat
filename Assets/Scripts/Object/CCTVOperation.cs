using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVOperation : MonoBehaviour
{
    public Material Screen;
    public GameObject Barrier;
    public GameObject Door;

    private float angle = -100;
    private bool isBarrierMove = false;
    private bool isPlayerCollision = false;
    private bool isDoorClose = false;

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

        if(isDoorClose)
        {
            CloseDoor();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isBarrierMove && isPlayerCollision)
        {
            Debug.Log("Input F");
            isBarrierMove = true;
            isDoorClose = true;
        }

    }

    private void CloseDoor()
    {
        angle++;

        Debug.Log(angle);
        if(angle > 0)
        {
            isDoorClose = false;
        }
        Door.transform.rotation = Quaternion.Euler(0, angle, 0);

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
