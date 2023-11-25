using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIndividualOperation : SwitchesOperation
{
    public int IndividualIndex;
    public GameObject Switchbone;
    private bool isSwitchOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            this[IndividualIndex] = !this[IndividualIndex];
            isSwitchOn = !isSwitchOn;

            // on : -40 , off : 40
            if (isSwitchOn)
            {
                Switchbone.transform.rotation = Quaternion.Euler(0, 40, 0);
            }
            else
            {
                Switchbone.transform.rotation = Quaternion.Euler(0, -40, 0);

            }

        }
    }

    // Test 
    void Update()
    {
        // 키패드에서 1번 키를 입력받으면 OnKeyPressed 메서드 호출
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this[IndividualIndex] = !this[IndividualIndex];
            isSwitchOn = !isSwitchOn;

            // on : -40 , off : 40
            if (isSwitchOn)
            {
                Switchbone.transform.rotation = Quaternion.Euler(0, 40, 0);
            }
            else
            {
                Switchbone.transform.rotation = Quaternion.Euler(0, -40, 0);

            }
        }
    }
}