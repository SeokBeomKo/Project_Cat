using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchsController : MonoBehaviour
{
    public SwitchOperation[] switches;

    private bool[] isSwitchsOn;

    [Header("스위치 작동시 움직일 대상")]
    public GameObject Door;
    public float DoorSpeed = 2;

    private bool isClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        isSwitchsOn = new bool[5];

        for (int i = 0; i < isSwitchsOn.Length; i++)
        {
            isSwitchsOn[i] = false;
        }

    }


    // Update is called once per frame
    void Update()
    {  
        for (int i = 0; i < switches.Length; i++)
        {
            SwitchOperation switchObject = switches[i];
            bool switchState = switchObject.GetSwitchState();
            isSwitchsOn[i] = switchState;

            //Debug.Log("스위치 " + i + switchState);
        }

        bool allSwitchesOn = isSwitchsOn.All(x => x);

        if (allSwitchesOn && !isClosed)
        {
            Debug.Log("모두 눌림 ");
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        Door.transform.Translate(Vector3.down * DoorSpeed * Time.deltaTime);
        if (Door.transform.position.y < 3)
        {
            isClosed = true;
        }
    }
}
