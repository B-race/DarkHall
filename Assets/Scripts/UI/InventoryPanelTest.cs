using UnityEngine;

public class InventoryPanelTest : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}