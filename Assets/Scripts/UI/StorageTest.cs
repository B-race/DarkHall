using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageTest : MonoBehaviour
{
    [SerializeField] private GameObject storagePanel;
    [SerializeField] private Image[] storageSlotImages;
    [SerializeField] private InventoryTest inventory;

    private int maxSlots = 15;

    private class StorageSlotData
    {
        public string itemName;
        public TestItemType itemType;
        public int quantity;

        public StorageSlotData(string name, TestItemType type)
        {
            itemName = name;
            itemType = type;
            quantity = 1;
        }
    }

    private List<StorageSlotData> slots =
        new List<StorageSlotData>();

    private void Start()
    {
        if (storagePanel != null)
        {
            storagePanel.SetActive(false);
        }

        SetupStorageButtons();
        UpdateStorageUI();
    }

    private void SetupStorageButtons()
    {
        for (int i = 0; i < storageSlotImages.Length; i++)
        {
            int index = i;

            Button button =
                storageSlotImages[i].GetComponent<Button>();

            if (button == null)
            {
                button =
                    storageSlotImages[i].gameObject.AddComponent<Button>();
            }

            button.targetGraphic = storageSlotImages[i];

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(
                () => TakeItemFromStorage(index)
            );
        }
    }

    public void OpenStorage()
    {
        if (storagePanel != null)
        {
            storagePanel.SetActive(true);
        }

        if (inventory != null)
        {
            inventory.BeginStorageMode(this);
        }

        Debug.Log("Storage Open");
    }

    public void CloseStorage()
    {
        if (storagePanel != null)
        {
            storagePanel.SetActive(false);
        }

        if (inventory != null)
        {
            inventory.EndStorageMode();
        }

        Debug.Log("Storage Close");
    }

    public void TryStoreItemFromInventory(int inventoryIndex)
    {
        if (inventory == null)
        {
            return;
        }

        string itemName =
            inventory.GetItemNameAt(inventoryIndex);

        TestItemType itemType =
            inventory.GetItemTypeAt(inventoryIndex);

        if (string.IsNullOrEmpty(itemName))
        {
            return;
        }

        bool added =
            AddItemToStorage(itemName, itemType);

        if (!added)
        {
            Debug.Log("Storage Full");
            return;
        }

        inventory.RemoveOneItemAt(inventoryIndex);

        Debug.Log(
            "Moved To Storage: " +
            itemName
        );
    }

    private bool AddItemToStorage(
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
                        "Storage Stacked: " +
                        itemName +
                        " x " +
                        slots[i].quantity
                    );

                    UpdateStorageUI();
                    return true;
                }
            }
        }

        if (slots.Count >= maxSlots)
        {
            return false;
        }

        slots.Add(
            new StorageSlotData(
                itemName,
                itemType
            )
        );

        UpdateStorageUI();

        return true;
    }

    private void TakeItemFromStorage(int storageIndex)
    {
        if (inventory == null)
        {
            return;
        }

        if (storageIndex < 0 ||
            storageIndex >= slots.Count)
        {
            return;
        }

        StorageSlotData selectedItem =
            slots[storageIndex];

        bool added =
            inventory.AddItem(
                selectedItem.itemName,
                selectedItem.itemType
            );

        if (!added)
        {
            Debug.Log(
                "Inventory Full - Cannot Take Item"
            );

            return;
        }

        selectedItem.quantity--;

        Debug.Log(
            "Moved To Inventory: " +
            selectedItem.itemName
        );

        if (selectedItem.quantity <= 0)
        {
            slots.RemoveAt(storageIndex);
        }

        UpdateStorageUI();
    }

    private int GetMaxStack(
        TestItemType itemType)
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

    private void UpdateStorageUI()
    {
        for (int i = 0;
             i < storageSlotImages.Length;
             i++)
        {
            storageSlotImages[i].color =
                i < slots.Count
                ? Color.white
                : Color.gray;
        }
    }
}