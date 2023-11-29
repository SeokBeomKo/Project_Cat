using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIndividualOperation : MonoBehaviour
{
    public SwitchesOperation switchesOperation;

    public int IndividualIndex;
    public GameObject Switchbone;
    private bool isSwitchOn = false;
    private float angle;

    public Room2GameCenter room2GameCenter;

    private void OnTriggerEnter(Collider other)
    {
        if (room2GameCenter.IsSwitchesUnlocked)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
            {

                switchesOperation[IndividualIndex, true] = !switchesOperation[IndividualIndex, true];
                isSwitchOn = !isSwitchOn;

                // on : -40 , off : 40
                if (isSwitchOn)
                {
                    angle = -50;
                }
                else
                {
                    angle = -130;
                }
                Debug.Log(angle);
                Switchbone.transform.rotation = Quaternion.Euler(angle, 0, 0);
                Debug.Log(Switchbone.transform.rotation.x);

            }
        }
    }

    // Test 
    //void Update()
    //{
    //    // 키패드에서 1번 키를 입력받으면 OnKeyPressed 메서드 호출
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        switchesOperation[IndividualIndex] = !switchesOperation[IndividualIndex];
    //        isSwitchOn = !isSwitchOn;

    //        // on : -40 , off : 40
    //        if (isSwitchOn)
    //        {
    //            angle = -50;
    //        }
    //        else
    //        {
    //            angle = -130;
    //        }
    //        Debug.Log(angle);
    //        Switchbone.transform.rotation = Quaternion.Euler(angle, 0, 0);
    //        Debug.Log(Switchbone.transform.rotation.x);
    //    }
    //}
}