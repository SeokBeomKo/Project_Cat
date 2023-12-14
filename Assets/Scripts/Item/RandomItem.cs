using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public string[] ItemNames =
    {
        "운동에너지",
        "보호막",
        "츄르",
        "털뭉치",
        "WaterBottle",
        "LifeEnergy"
    };

    public delegate void RandomItemHandle(string itemName);
    public event RandomItemHandle OnRandomItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemNames != null && ItemNames.Length > 0)
            {
                SoundManager.Instance.PlaySFX("GetItem");

                string itemName = GetRandomName();

                if (itemName == "WaterBottle")
                {
                    OnRandomItem?.Invoke("WaterBottle");
                    Debug.Log("[RandomItem] WaterBottle : 탄약 충전");
                }
                else if (itemName == "LifeEnergy")
                {
                    OnRandomItem?.Invoke("LifeEnergy");
                    Debug.Log("[RandomItem] LifeEnergy : 플레이어 HP 충전");
                }
                else
                {
                    InventoryManager.Instance.AddItemToInventory(itemName);
                    Debug.Log("[RandomItem] Randomly selected name: " + itemName);
                }
                gameObject.SetActive(false);
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
