using System.Collections.Generic;
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

    private class Recipe
    {
        public string material1;
        public string material2;
        public string result;

        public Recipe(string material1, string material2, string result)
        {
            this.material1 = material1;
            this.material2 = material2;
            this.result = result;
        }
    }

    private List<Recipe> recipes = new List<Recipe>();

    private string selectedMaterial1 = "";
    private string selectedMaterial2 = "";
    private Recipe selectedRecipe = null;

    private void Start()
    {
        AddRecipes();

        if (craftPanel != null)
        {
            craftPanel.SetActive(false);
        }

        ResetCraft();
    }

    private void AddRecipes()
    {
        // 일반
        recipes.Add(new Recipe("막대기", "클립", "자물쇠 따개"));
        recipes.Add(new Recipe("유리 파편", "플라스틱 조각", "표창"));
        recipes.Add(new Recipe("막대기", "밧줄", "몽둥이"));
        recipes.Add(new Recipe("성냥", "노란 한지", "잿가루"));
        recipes.Add(new Recipe("클립", "노란 한지", "종이꽃"));

        // 희귀
        recipes.Add(new Recipe("청주", "성냥", "화염병"));
        recipes.Add(new Recipe("막대기", "벼루", "해머"));
        recipes.Add(new Recipe("노란 한지", "모터", "바람개비"));
        recipes.Add(new Recipe("유리 파편", "부러진 단검", "투척용 단검"));
        recipes.Add(new Recipe("향", "성냥", "디퓨저"));

        // 전설
        recipes.Add(new Recipe("모터", "둥근 톱날", "절단기"));
        recipes.Add(new Recipe("노란 한지", "붉게 물든 붓", "부적"));
        recipes.Add(new Recipe("벼루", "붉게 물든 붓", "엑스칼리붓"));
        recipes.Add(new Recipe("모터", "검은 장우산", "스파이의 무기 레플리카"));
    }

    public void OpenCraftPanel()
    {
        if (craftPanel != null)
        {
            craftPanel.SetActive(true);
        }

        ResetCraft();

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

    // 현재 테스트 버튼용
    public void SelectMaterial1()
    {
        SelectFirstMaterial("막대기");
    }

    // 현재 테스트 버튼용
    public void SelectMaterial2()
    {
        SelectSecondMaterial("클립");
    }

    public void SelectFirstMaterial(string itemName)
    {
        if (inventory == null)
        {
            return;
        }

        if (inventory.GetItemCount(itemName) < 1)
        {
            Debug.Log("Not enough: " + itemName);
            return;
        }

        selectedMaterial1 = itemName;
        selectedMaterial2 = "";
        selectedRecipe = null;

        if (material1Button != null)
        {
            material1Button.interactable = false;
        }

        if (material2Button != null)
        {
            material2Button.interactable = true;
        }

        if (craftButton != null)
        {
            craftButton.interactable = false;
        }

        UpdateResultUI();

        Debug.Log("Material 1 Selected: " + itemName);
    }

    public void SelectSecondMaterial(string itemName)
    {
        if (string.IsNullOrEmpty(selectedMaterial1))
        {
            return;
        }

        int needCount = 1;

        if (selectedMaterial1 == itemName)
        {
            needCount = 2;
        }

        if (inventory.GetItemCount(itemName) < needCount)
        {
            Debug.Log("Not enough: " + itemName);
            return;
        }

        Recipe recipe = FindRecipe(selectedMaterial1, itemName);

        if (recipe == null)
        {
            Debug.Log("Cannot combine: "
                + selectedMaterial1
                + " + "
                + itemName);

            return;
        }

        selectedMaterial2 = itemName;
        selectedRecipe = recipe;

        if (material2Button != null)
        {
            material2Button.interactable = false;
        }

        if (craftButton != null)
        {
            craftButton.interactable = true;
        }

        UpdateResultUI();

        Debug.Log("Material 2 Selected: " + itemName);

        if (IsRecipeDiscovered(selectedRecipe))
        {
            Debug.Log("Recipe Result: " + selectedRecipe.result);
        }
        else
        {
            Debug.Log("Recipe Result: ???");
        }
    }

    public void CraftItem()
    {
        if (selectedRecipe == null)
        {
            return;
        }

        if (!HasRequiredMaterials())
        {
            Debug.Log("Not enough materials.");
            ResetCraft();
            return;
        }

        bool removedFirst = inventory.RemoveItem(selectedMaterial1, 1);

        if (!removedFirst)
        {
            return;
        }

        bool removedSecond = inventory.RemoveItem(selectedMaterial2, 1);

        if (!removedSecond)
        {
            inventory.AddItem(
                selectedMaterial1,
                TestItemType.Material
            );

            return;
        }

        bool added = inventory.AddItem(
            selectedRecipe.result,
            TestItemType.Tool
        );

        if (!added)
        {
            inventory.AddItem(
                selectedMaterial1,
                TestItemType.Material
            );

            inventory.AddItem(
                selectedMaterial2,
                TestItemType.Material
            );

            Debug.Log("Inventory Full - Craft Cancelled");

            ResetCraft();
            return;
        }

        PlayerPrefs.SetInt(
            "CraftRecipe_" + selectedRecipe.result,
            1
        );

        PlayerPrefs.Save();

        Debug.Log(
            "Craft Complete: "
            + selectedRecipe.result
        );

        ResetCraft();
    }

    public bool IsCompatibleMaterial(string itemName)
    {
        if (string.IsNullOrEmpty(selectedMaterial1))
        {
            return false;
        }

        return FindRecipe(selectedMaterial1, itemName) != null;
    }

    private Recipe FindRecipe(string material1, string material2)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            Recipe recipe = recipes[i];

            bool normalOrder =
                recipe.material1 == material1 &&
                recipe.material2 == material2;

            bool reverseOrder =
                recipe.material1 == material2 &&
                recipe.material2 == material1;

            if (normalOrder || reverseOrder)
            {
                return recipe;
            }
        }

        return null;
    }

    private bool HasRequiredMaterials()
    {
        if (selectedMaterial1 == selectedMaterial2)
        {
            return inventory.GetItemCount(selectedMaterial1) >= 2;
        }

        return
            inventory.GetItemCount(selectedMaterial1) >= 1 &&
            inventory.GetItemCount(selectedMaterial2) >= 1;
    }

    private bool IsRecipeDiscovered(Recipe recipe)
    {
        if (recipe == null)
        {
            return false;
        }

        return PlayerPrefs.GetInt(
            "CraftRecipe_" + recipe.result,
            0
        ) == 1;
    }

    private void UpdateResultUI()
    {
        if (selectedRecipe == null)
        {
            if (unknownResultPanel != null)
            {
                unknownResultPanel.SetActive(false);
            }

            if (knownResultPanel != null)
            {
                knownResultPanel.SetActive(false);
            }

            return;
        }

        bool discovered = IsRecipeDiscovered(selectedRecipe);

        if (unknownResultPanel != null)
        {
            unknownResultPanel.SetActive(!discovered);
        }

        if (knownResultPanel != null)
        {
            knownResultPanel.SetActive(discovered);
        }
    }

    private void ResetCraft()
    {
        selectedMaterial1 = "";
        selectedMaterial2 = "";
        selectedRecipe = null;

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

        UpdateResultUI();
    }
}