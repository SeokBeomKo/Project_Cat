using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//enum ItemName {Chur}

public class InventoryManager : Singleton<InventoryManager>
{
    // 인벤토리 아이템을 관리할 리스트
    [SerializeField]
    public List<Item> inventory = new List<Item>();

    // 아이템 클래스
    [Serializable]
    public class Item
    {
        public string name;
        public int quantity;
        public int maxQuantity;

        public Item(string name)
        {
            this.name = name;
            quantity = 0;
        }
    }

    private void Start()
    {
        // 여러 아이템을 미리 정의하고 인벤토리에 추가
        AddItemToInventory("츄르");
        AddItemToInventory("운동에너지");
        AddItemToInventory("보호막");
        AddItemToInventory("털뭉치");
        AddItemToInventory("물폭탄");
        AddItemToInventory("세척탄");

        // 아이템 최대 개수 설정
        SetItemMaxQuantity("츄르", 3); // 3
        SetItemMaxQuantity("운동에너지", 1); // 1
        SetItemMaxQuantity("보호막", 2); // 1
        SetItemMaxQuantity("털뭉치", 3); // 3
        SetItemMaxQuantity("물폭탄", 3); // 3
        SetItemMaxQuantity("세척탄", 3); // 3
    }

    public void SetItemMaxQuantity(string itemName, int maxQuantity)
    {
        Item existingItem = inventory.Find(item => item.name == itemName);

        if(existingItem!=null)
        {
            existingItem.maxQuantity = maxQuantity;
        }
        
    }

    // 아이템을 인벤토리에 추가하는 함수
    public void AddItemToInventory(string itemName)
    {
        // 이미 인벤토리에 아이템이 있는지 확인
        Item existingItem = inventory.Find(item => item.name == itemName);

        if (existingItem != null)
        {
            if (existingItem.quantity < existingItem.maxQuantity)
            {
                existingItem.quantity++;
                Debug.Log("[InventoryManager]" + itemName + " : " + existingItem.quantity);

            }
            else
            {
                Debug.Log( "[InventoryManager]" + itemName + " 해당 아이템 보유 개수 초과 ");
            }

        }
        else
        {
            // 새로운 아이템을 추가
            inventory.Add(new Item(itemName));
        }
    }

    // 아이템을 사용하는 함수
    public void UseItem(string itemName)
    {
        Item item = inventory.Find(item => item.name == itemName);

        if (item != null && item.quantity > 0)
        {
            item.quantity--;
        }
        else
        {
            Debug.Log("아이템을 사용할 수 없음: " + itemName);
        }
    }

    public int GetItemCount(string itemName)
    {
        Item item = inventory.Find(item => item.name == itemName);
        if (null == item) return 0;
        return item.quantity;
    }
}

