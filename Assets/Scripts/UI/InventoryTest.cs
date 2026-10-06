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

    [Header("아이템 상세창")]
    [SerializeField] private GameObject itemDetailPanel;
    [SerializeField] private Button discardButton;

    private int selectedSlotIndex = -1;

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

        if (itemDetailPanel != null)
        {
            itemDetailPanel.SetActive(false);
        }

        SetupSlotButtons();
        UpdateInventoryUI();
    }

    private void SetupSlotButtons()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            int index = i;

            Button button = slotImages[i].GetComponent<Button>();

            if (button == null)
            {
                button = slotImages[i].gameObject.AddComponent<Button>();
            }

            button.targetGraphic = slotImages[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectSlot(index));
        }
    }

    public bool AddItem(string itemName, TestItemType itemType)
    {
        int maxStack = GetMaxStack(itemType);

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

        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventory Full");

            if (inventoryFullPanel != null)
            {
                inventoryFullPanel.SetActive(true);
            }

            return false;
        }

        slots.Add(new InventorySlotData(itemName, itemType));

        Debug.Log("Item Acquired: " + itemName);
        Debug.Log("Inventory Slots: " + slots.Count + " / " + maxSlots);

        UpdateInventoryUI();
        return true;
    }

    private void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Count)
        {
            return;
        }

        selectedSlotIndex = index;

        InventorySlotData selectedItem = slots[selectedSlotIndex];

        Debug.Log(
            "Selected Item: " +
            selectedItem.itemName +
            " / Quantity: " +
            selectedItem.quantity
        );

        if (discardButton != null)
        {
            discardButton.interactable =
                selectedItem.itemType == TestItemType.Material;
        }

        if (itemDetailPanel != null)
        {
            itemDetailPanel.SetActive(true);
        }
    }

    public void DiscardSelectedItem()
    {
        if (selectedSlotIndex < 0 ||
            selectedSlotIndex >= slots.Count)
        {
            return;
        }

        InventorySlotData selectedItem = slots[selectedSlotIndex];

        if (selectedItem.itemType != TestItemType.Material)
        {
            Debug.Log("Only material items can be discarded.");
            return;
        }

        selectedItem.quantity--;

        Debug.Log(
            "Item Discarded: " +
            selectedItem.itemName +
            " / Remaining: " +
            selectedItem.quantity
        );

        if (selectedItem.quantity <= 0)
        {
            slots.RemoveAt(selectedSlotIndex);
        }

        selectedSlotIndex = -1;

        UpdateInventoryUI();
        CloseItemDetailPanel();
    }

    public void CloseItemDetailPanel()
    {
        selectedSlotIndex = -1;

        if (itemDetailPanel != null)
        {
            itemDetailPanel.SetActive(false);
        }
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