using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVOperation : MonoBehaviour
{
    public Material Screen;
    public GameObject Barrier;
    public GameObject Door;
    public Room2GameCenter room2GameCenter;
    public GameObject interactiveKey;

    private float angle = -100;
    private bool isBarrierMove = false;
    private bool isPlayerCollision = false;
    private bool isDoorClose = false;
    private bool hasput = false;

    private bool isUsed = false;
    private bool isBarrier = true;

    public delegate void CCTVHandle();
    public event CCTVHandle OnCloseDoorTrue;
    public event CCTVHandle OnCloseDoorFalse;

    public event CCTVHandle OnCloseBarrierTrue;
    public event CCTVHandle OnCloseBarrierFalse;

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

        if(isDoorClose && !isBarrierMove)
        {
            CloseDoor();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isBarrierMove && isPlayerCollision && !isUsed)
        {
            isUsed = true;
            isBarrierMove = true;
            
            
            room2GameCenter.IsSwitchesUnlocked = true;
            
            hasput = true;
            interactiveKey.SetActive(false);
        }

    }

    private void CloseDoor()
    {
        OnCloseDoorTrue?.Invoke();
        angle++;

        if(angle > 0)
        {
            isDoorClose = false;
            Invoke("SuccessCloseDoor", 3f);
        }
        Door.transform.rotation = Quaternion.Euler(0, angle, 0);


    }

    private void SuccessCloseDoor()
    {
        OnCloseDoorFalse?.Invoke();
    }

    private void SuccessMoveBarrier()
    {
        OnCloseBarrierTrue?.Invoke();
    }
    private void MoveBarrier()
    {
        Vector3 barrierPosition = Barrier.transform.localPosition;
        if (barrierPosition.y > 0.14f)
        {
            Barrier.transform.position += new Vector3(0, -0.1f, 0) * Time.deltaTime * 8f;
            if (isBarrier)
            {
                Invoke("SuccessMoveBarrier", 2f);
                isBarrier = false;
            }
        }
        else
        {
            isBarrierMove = false;
            isDoorClose = true;
            OnCloseDoorTrue?.Invoke();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerCollision = true;
            Screen.EnableKeyword("_EMISSION");
            
            if (!hasput)
            {
                interactiveKey.SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerCollision = false;
        
        if (!hasput)
        {
            interactiveKey.SetActive(false);
        }
    }
}
