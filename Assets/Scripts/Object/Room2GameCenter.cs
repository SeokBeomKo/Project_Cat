using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2GameCenter : MonoBehaviour
{
    public GameObject Door;
    public GameObject Fog;

    private bool isSwitchesUnlocked = false;
    private bool isDoorOpen = false;
    private float angle = 0;

    public bool IsSwitchesUnlocked
    {
        get
        {
            return isSwitchesUnlocked;
        }
        set
        {
            isSwitchesUnlocked = value;
        }

    }

    public bool IsDoorOpen
    {
        get
        {
            return isDoorOpen;
        }
        set
        {
            isDoorOpen = value;
        }
    }

    private void Update()
    {

        if(isDoorOpen)
        {
            OpenDoor();
            Fog.SetActive(false);
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
        Fog.SetActive(false);
    }
}
