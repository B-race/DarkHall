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
    [SerializeField] private Button consumeButton;
    [SerializeField] private Button equipButton;

    [Header("플레이어 상태")]
    [SerializeField] private PlayerStatusTest playerStatus;

    private int selectedSlotIndex = -1;
    private string equippedToolName = "";

    private StorageTest activeStorage;

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

    private List<InventorySlotData> slots =
        new List<InventorySlotData>();

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

            Button button =
                slotImages[i].GetComponent<Button>();

            if (button == null)
            {
                button =
                    slotImages[i].gameObject.AddComponent<Button>();
            }

            button.targetGraphic = slotImages[i];

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(
                () => SelectSlot(index)
            );
        }
    }

    public bool AddItem(
        string itemName,
        TestItemType itemType)
    {
        int maxStack =
            GetMaxStack(itemType);

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

        slots.Add(
            new InventorySlotData(
                itemName,
                itemType
            )
        );

        Debug.Log(
            "Item Acquired: " +
            itemName
        );

        UpdateInventoryUI();

        return true;
    }

    public int GetItemCount(string itemName)
    {
        int total = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemName == itemName)
            {
                total += slots[i].quantity;
            }
        }

        return total;
    }

    public bool RemoveItem(
        string itemName,
        int amount)
    {
        if (GetItemCount(itemName) < amount)
        {
            return false;
        }

        int remaining = amount;

        for (int i = slots.Count - 1;
             i >= 0;
             i--)
        {
            if (slots[i].itemName != itemName)
            {
                continue;
            }

            if (slots[i].quantity > remaining)
            {
                slots[i].quantity -= remaining;
                remaining = 0;
            }
            else
            {
                remaining -= slots[i].quantity;
                slots.RemoveAt(i);
            }

            if (remaining <= 0)
            {
                break;
            }
        }

        UpdateInventoryUI();

        return true;
    }

    public string GetItemNameAt(int index)
    {
        if (index < 0 ||
            index >= slots.Count)
        {
            return "";
        }

        return slots[index].itemName;
    }

    public TestItemType GetItemTypeAt(int index)
    {
        if (index < 0 ||
            index >= slots.Count)
        {
            return TestItemType.Material;
        }

        return slots[index].itemType;
    }

    public bool RemoveOneItemAt(int index)
    {
        if (index < 0 ||
            index >= slots.Count)
        {
            return false;
        }

        slots[index].quantity--;

        if (slots[index].quantity <= 0)
        {
            slots.RemoveAt(index);
        }

        UpdateInventoryUI();

        return true;
    }

    public void BeginStorageMode(
        StorageTest storageManager)
    {
        activeStorage = storageManager;

        CloseItemDetailPanel();
    }

    public void EndStorageMode()
    {
        activeStorage = null;
    }

    private void SelectSlot(int index)
    {
        if (index < 0 ||
            index >= slots.Count)
        {
            return;
        }

        // 창고가 열려 있으면 상세창 대신 창고로 이동
        if (activeStorage != null)
        {
            activeStorage
                .TryStoreItemFromInventory(index);

            return;
        }

        selectedSlotIndex = index;

        InventorySlotData selectedItem =
            slots[selectedSlotIndex];

        Debug.Log(
            "Selected Item: " +
            selectedItem.itemName +
            " / Quantity: " +
            selectedItem.quantity
        );

        if (discardButton != null)
        {
            discardButton.interactable =
                selectedItem.itemType ==
                TestItemType.Material;
        }

        if (consumeButton != null)
        {
            consumeButton.interactable =
                selectedItem.itemType ==
                TestItemType.Food;
        }

        if (equipButton != null)
        {
            equipButton.interactable =
                selectedItem.itemType ==
                TestItemType.Tool;
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

        InventorySlotData selectedItem =
            slots[selectedSlotIndex];

        if (selectedItem.itemType !=
            TestItemType.Material)
        {
            return;
        }

        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            slots.RemoveAt(selectedSlotIndex);
        }

        selectedSlotIndex = -1;

        UpdateInventoryUI();
        CloseItemDetailPanel();
    }

    public void ConsumeSelectedFood()
    {
        if (selectedSlotIndex < 0 ||
            selectedSlotIndex >= slots.Count)
        {
            return;
        }

        InventorySlotData selectedItem =
            slots[selectedSlotIndex];

        if (selectedItem.itemType !=
            TestItemType.Food)
        {
            return;
        }

        if (playerStatus != null &&
            playerStatus.IsHPFull())
        {
            Debug.Log("HP is already full.");
            return;
        }

        selectedItem.quantity--;

        if (playerStatus != null)
        {
            playerStatus.HealHP(20);
        }

        if (selectedItem.quantity <= 0)
        {
            slots.RemoveAt(selectedSlotIndex);
        }

        selectedSlotIndex = -1;

        UpdateInventoryUI();
        CloseItemDetailPanel();
    }

    public void EquipSelectedTool()
    {
        if (selectedSlotIndex < 0 ||
            selectedSlotIndex >= slots.Count)
        {
            return;
        }

        InventorySlotData selectedItem =
            slots[selectedSlotIndex];

        if (selectedItem.itemType !=
            TestItemType.Tool)
        {
            return;
        }

        equippedToolName =
            selectedItem.itemName;

        Debug.Log(
            "Tool Equipped: " +
            equippedToolName
        );

        selectedSlotIndex = -1;

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

    private int GetMaxStack(
        TestItemType itemType)
    {
        if (itemType ==
            TestItemType.Material)
        {
            return 3;
        }

        if (itemType ==
            TestItemType.Food)
        {
            return 5;
        }

        return 1;
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0;
             i < slotImages.Length;
             i++)
        {
            slotImages[i].color =
                i < slots.Count
                ? Color.white
                : Color.gray;
        }
    }
}