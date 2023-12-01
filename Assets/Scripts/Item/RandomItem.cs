using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public string[] ItemNames =
    {
        "Chur",
        "ProtectEnergy",
        "WaterBomb",
        "ClearBomb",
        "KineticEnergy",
        "Hairball"
    };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemNames != null && ItemNames.Length > 0)
            {
                string itemName = GetRandomName();

                if (itemName == "WaterBottle")
                {
                    Debug.Log("탄약 충전");
                }
                else if(itemName == "LifeEnergy")
                {
                    Debug.Log("플레이어 HP 충전");
                }

                Debug.Log("item add : " + itemName);
                InventoryManager.Instance.AddItemToInventory(itemName);
                gameObject.SetActive(false);
                Debug.Log("Randomly selected name: " + itemName);
            }
            else
            {
                Debug.LogError("No names provided. Please assign an array of names to the script.");
            }
        }
    }

    string GetRandomName()
    {
        int randomIndex = UnityEngine.Random.Range(0, ItemNames.Length);
        return ItemNames[randomIndex];
    }
}
