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
        /*"물폭탄",
        "세척탄",*/
        "츄르",
        "털뭉치"
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
                    Debug.Log("[RandomItem] 탄약 충전");
                }
                else if(itemName == "LifeEnergy")
                {
                    OnRandomItem?.Invoke("LifeEnergy");
                    Debug.Log("[RandomItem] 플레이어 HP 충전");
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
