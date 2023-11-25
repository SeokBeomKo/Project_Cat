using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesOperation : MonoBehaviour
{
    public GameObject Door;
    public GameObject[] SwitchBones;

    private bool[] switchesState;

    void Start()
    {
        // 스위치 초기화
        switchesState = new bool[5];
        for (int i = 0; i < switchesState.Length; i++)
        {
            switchesState[i] = false;
        }
    }

    public bool this[int index]
    {
        get
        {
            return switchesState[index];
        }
        set
        {
            switchesState[index] = value;
            Debug.Log("index : " + index + ", " + switchesState[index]);
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
