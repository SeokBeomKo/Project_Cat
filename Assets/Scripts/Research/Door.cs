using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    public GameObject fog;

    private float angle = -100;
    
    private bool isDoorClose = false;
    private bool isDoorOpen = false;

    private void CloseDoor()
    {
        angle++;

        if (angle > 0)
        {
            isDoorClose = true;
        }
        door.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void OpenDoor()
    {
        angle--;

        if (angle < -100)
        {
            isDoorOpen = true;
        }
        door.transform.rotation = Quaternion.Euler(0, angle, 0);
        fog.SetActive(false);
    }
}
