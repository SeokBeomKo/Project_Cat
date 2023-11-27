using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchesOperation : MonoBehaviour
{
    public Room2GameCenter room2GameCenter;

    public GameObject Door;
    private bool[] switchesState;
    int childCount;

    void Start()
    {
        childCount = transform.childCount;

        // 스위치 초기화
        switchesState = new bool[childCount];
        for (int i = 0; i < switchesState.Length; i++)
        {
            switchesState[i] = false;
        }
    }

    public bool this[int index, bool link = false]
    {
        get
        {
            return switchesState[index];
        }
        set
        {
            switchesState[index] = value;

            Debug.Log("index : " + index + ", " + switchesState[index]);

            if (link)
            {
                if (index > 0)
                {
                    switchesState[index - 1] = !switchesState[index - 1];
                    Debug.Log(index + " -1 " + switchesState[index - 1]);

                }
                else if (index < switchesState.Length)
                {
                    switchesState[index + 1] = !switchesState[index + 1];
                    Debug.Log(index + " +1 " + switchesState[index + 1]);

                }
            }
           
        }
    }

    private void Update()
    {
        bool allTrue = true;
        for (int i = 0; i < switchesState.Length; i++)
        {
            if(switchesState[i] == false)
            {
                allTrue = false;
                break;
            }
        }

        if (allTrue)
        {
            room2GameCenter.IsDoorOpen = true;
        }
    }
 

}
