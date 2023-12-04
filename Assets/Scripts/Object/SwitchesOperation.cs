using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SwitchesOperation : MonoBehaviour
{
    private Room2GameCenter room2GameCenter;
    public GameObject Door;
    private bool[] switchesState;
    public GameObject[] Switchbones;
    private float angle;

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

    public bool GetSwitch(int index)
    {
        return switchesState[index];
    }

    public void SetSwitch(int index, bool link)
    {
        switchesState[index] = !switchesState[index];
        SwitchChange(index);
        
        CheckSwitch();
        
        Debug.Log(index+ "번 스위치 " + " : " + !switchesState[index]+ "->" + switchesState[index]);

        if (link)
        {
            Debug.Log("양옆 변경 ");
            if (index > 0)
            {
                SetSwitch(index - 1, false);

            }

            if (index < switchesState.Length - 1)
            {
                SetSwitch(index + 1, false);
            }
        }

        
    }

    private void CheckSwitch()
    {
        bool allTrue = true;
        for (int i = 0; i < switchesState.Length; i++)
        {
            if (!switchesState[i])
            {
                allTrue = false;
                break;
            }
        }

        if (allTrue)
        {
            Debug.Log("문 열림 전달 ");
            room2GameCenter.IsDoorOpen = true;
        }
    }

    public void SwitchChange(int index)
    {
        // on : -40 , off : 40
        if (GetSwitch(index))
        {
            angle = -50;
        }
        else
        {
            angle = -130;
        }
        Switchbones[index].transform.rotation = Quaternion.Euler(angle, 0, 0);
    }
 

}
