using UnityEngine;

public class CraftingTestHelper : MonoBehaviour
{
    [SerializeField] private InventoryTest inventory;

    public void AddWood()
    {
        if (inventory == null)
        {
            return;
        }

        inventory.AddItem("Wood", TestItemType.Material);

        Debug.Log("Wood Added");
    }
}