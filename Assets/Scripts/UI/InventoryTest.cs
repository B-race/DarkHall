using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TestItemType
{
    Material,
    Food,
    Tool
}

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private int maxSlots = 9;
    [SerializeField] private Image[] slotImages;

    private class InventorySlotData
    {
        public string itemName;
        public TestItemType itemType;
        public int quantity;

        public InventorySlotData(string name, TestItemType type)
        {
            itemName = name;
            itemType = type;
            quantity = 1;
        }
    }

    private List<InventorySlotData> slots = new List<InventorySlotData>();

    private void Start()
    {
        UpdateInventoryUI();
    }

    public bool AddItem(string itemName, TestItemType itemType)
    {
        int maxStack = GetMaxStack(itemType);

        // 중첩 가능한 아이템이면 기존 슬롯부터 확인
        if (maxStack > 1)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].itemName == itemName &&
                    slots[i].itemType == itemType &&
                    slots[i].quantity < maxStack)
                {
                    slots[i].quantity++;

                    Debug.Log(
                        "Item Stacked: " +
                        itemName +
                        " x " +
                        slots[i].quantity
                    );

                    UpdateInventoryUI();
                    return true;
                }
            }
        }

        // 새 슬롯이 필요한데 인벤토리가 가득 찬 경우
        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventory Full");
            return false;
        }

        // 새 슬롯 생성
        InventorySlotData newSlot =
            new InventorySlotData(itemName, itemType);

        slots.Add(newSlot);

        Debug.Log("Item Acquired: " + itemName);
        Debug.Log("Inventory Slots: " + slots.Count + " / " + maxSlots);

        UpdateInventoryUI();

        return true;
    }

    private int GetMaxStack(TestItemType itemType)
    {
        if (itemType == TestItemType.Material)
        {
            return 3;
        }

        if (itemType == TestItemType.Food)
        {
            return 5;
        }

        return 1;
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < slots.Count)
            {
                slotImages[i].color = Color.white;
            }
            else
            {
                slotImages[i].color = Color.gray;
            }
        }
    }
}