using UnityEngine;
using UnityEngine.UI;

public class CraftPanelTest : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel;

    [Header("제작 버튼")]
    [SerializeField] private Button material1Button;
    [SerializeField] private Button material2Button;
    [SerializeField] private Button craftButton;

    [Header("제작 결과")]
    [SerializeField] private GameObject unknownResultPanel;
    [SerializeField] private GameObject knownResultPanel;

    [Header("인벤토리")]
    [SerializeField] private InventoryTest inventory;

    private int selectedMaterialCount = 0;
    private bool recipeDiscovered = false;

    private void Start()
    {
        if (craftPanel != null)
        {
            craftPanel.SetActive(false);
        }

        UpdateResultUI();
        ResetCraft();
    }

    public void OpenCraftPanel()
    {
        if (craftPanel != null)
        {
            craftPanel.SetActive(true);
        }

        ResetCraft();
        UpdateResultUI();

        Debug.Log("Craft Panel Open");
    }

    public void CloseCraftPanel()
    {
        if (craftPanel != null)
        {
            craftPanel.SetActive(false);
        }

        ResetCraft();

        Debug.Log("Craft Panel Close");
    }

    public void SelectMaterial1()
    {
        if (inventory.GetItemCount("Wood") < 1)
        {
            Debug.Log("Not enough Wood.");
            return;
        }

        selectedMaterialCount = 1;

        material1Button.interactable = false;
        material2Button.interactable = true;
        craftButton.interactable = false;

        Debug.Log("Material 1 Selected: Wood");
    }

    public void SelectMaterial2()
    {
        if (selectedMaterialCount != 1)
        {
            return;
        }

        if (inventory.GetItemCount("Wood") < 2)
        {
            Debug.Log("Need 2 Wood.");
            return;
        }

        selectedMaterialCount = 2;

        material2Button.interactable = false;
        craftButton.interactable = true;

        Debug.Log("Material 2 Selected: Wood");

        if (recipeDiscovered)
        {
            Debug.Log("Recipe Result: CraftedTool");
        }
        else
        {
            Debug.Log("Recipe Result: ???");
        }
    }

    public void CraftItem()
    {
        if (selectedMaterialCount != 2)
        {
            return;
        }

        if (inventory.GetItemCount("Wood") < 2)
        {
            Debug.Log("Not enough Wood.");
            ResetCraft();
            return;
        }

        bool removed = inventory.RemoveItem("Wood", 2);

        if (!removed)
        {
            return;
        }

        bool added = inventory.AddItem(
            "CraftedTool",
            TestItemType.Tool
        );

        if (!added)
        {
            inventory.AddItem(
                "Wood",
                TestItemType.Material
            );

            inventory.AddItem(
                "Wood",
                TestItemType.Material
            );

            Debug.Log("Inventory Full - Craft Cancelled");

            ResetCraft();
            return;
        }

        recipeDiscovered = true;

        Debug.Log("Craft Complete: CraftedTool");

        UpdateResultUI();
        ResetCraft();
    }

    private void UpdateResultUI()
    {
        if (unknownResultPanel != null)
        {
            unknownResultPanel.SetActive(!recipeDiscovered);
        }

        if (knownResultPanel != null)
        {
            knownResultPanel.SetActive(recipeDiscovered);
        }
    }

    private void ResetCraft()
    {
        selectedMaterialCount = 0;

        if (material1Button != null)
        {
            material1Button.interactable = true;
        }

        if (material2Button != null)
        {
            material2Button.interactable = false;
        }

        if (craftButton != null)
        {
            craftButton.interactable = false;
        }
    }
}