using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Door;
    private bool isDoorOpen = false;
    private float angle = 0;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Input N");
            isDoorOpen = true;
        }

        if(isDoorOpen)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        angle--;

        if (angle < -100)
        {
            isDoorOpen = false;
        }
        Door.transform.rotation = Quaternion.Euler(0, angle, 0);

    }
}