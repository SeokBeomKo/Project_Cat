using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    public ItemWheel itemWheel;

    private void Start()
    {
        itemWheel.onItemClick += ClickTrue;
    }

    public void ClickTrue(string itemName)
    {
        switch(itemName)
        {
            case "보호막":
                Debug.Log("[ItemCenter] 보호에너지 선택");
                break;
            case "이동속도":
                Debug.Log("[ItemCenter] 운동에너지 - 이동속도 선택");
                break;
            case "공격력":
                Debug.Log("[ItemCenter] 운동에너지 - 공격력 선택");
                break;
            case "털뭉치":
                Debug.Log("[ItemCenter] 털뭉치 선택");
                break;
            case "츄르":
                Debug.Log("[ItemCenter] 츄르 선택");
                break;
        }
    }
}
