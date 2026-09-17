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

    [Header("인벤토리 가득 참 팝업")]
    [SerializeField] private GameObject inventoryFullPanel;

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
        if (inventoryFullPanel != null)
        {
            inventoryFullPanel.SetActive(false);
        }

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

        // 새로운 슬롯이 필요한데 인벤토리가 가득 찬 경우
        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventory Full");

            if (inventoryFullPanel != null)
            {
                inventoryFullPanel.SetActive(true);
            }

            return false;
        }

        // 빈 슬롯이 있으면 새 아이템 추가
        slots.Add(new InventorySlotData(itemName, itemType));

        Debug.Log("Item Acquired: " + itemName);
        Debug.Log("Inventory Slots: " + slots.Count + " / " + maxSlots);

        UpdateInventoryUI();
        return true;
    }

    public void CloseInventoryFullPanel()
    {
        if (inventoryFullPanel != null)
        {
            inventoryFullPanel.SetActive(false);
        }
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
            slotImages[i].color =
                i < slots.Count ? Color.white : Color.gray;
        }
    }
}