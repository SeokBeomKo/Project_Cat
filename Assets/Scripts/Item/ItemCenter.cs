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
        Debug.Log(itemName + "º±≈√");
    }
}
