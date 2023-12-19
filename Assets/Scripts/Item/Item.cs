using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public delegate void ItemHandle(string itemName);
    public event ItemHandle OnItem;
    [SerializeField]
    private bool isSave;
    public string itemCode;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            string itemName = itemCode;

            // "(Clone)" 문자열을 제거
            if (itemName.EndsWith("(Clone)"))
            {
                itemName = itemName.Substring(0, itemName.Length - 7); // 7은 "(Clone)" 문자열의 길이입니다.
            }

            if(isSave)
            {
                InventoryManager.Instance.AddItemToInventory(itemName);
            }
 
            gameObject.SetActive(false);
            OnItem?.Invoke(itemName);

        }
    }
}
