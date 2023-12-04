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
            switchesState[i] = true;
        }
    }

    private void Update()
    {
        //CheckSwitch();
    }

    public bool GetSwitch(int index)
    {
        return switchesState[index];
    }

    public void SetSwitch(int index, bool link)
    {
        switchesState[index] = !switchesState[index];
        SwitchChange(index); // 애니메이션       
        
        CheckSwitch();

        Debug.Log(index+ "번 스위치 " + " : " + !switchesState[index]+ "->" + switchesState[index]);

        if (link)
        {
            Debug.Log("양옆 변경 ");
            if (index > 0)
            {
                SetSwitch(index - 1, false); // 왼쪽
            }

            if (index < switchesState.Length - 1)
            {
                SetSwitch(index + 1, false); // 오른쪽
            }
        }
    }

    private void CheckSwitch()
    {
        int switchCount = 0;
        for (int i = 0; i < switchesState.Length; i++)
        {
            if (switchesState[i]) // 꺼짐
            {
                break;
            }
            else
            {
                switchCount++;
            }
        }

        if (switchCount == 5)
        {
            Debug.Log("문 열림 전달 ");
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
