using UnityEngine;
using UnityEngine.UI;

public class AltarTest : MonoBehaviour
{
    [Header("제사상 UI")]
    [SerializeField] private GameObject altarPanel;
    [SerializeField] private GameObject altarConfirmPanel;
    [SerializeField] private Slider altarGauge;

    [Header("제사상 오브젝트")]
    [SerializeField] private GameObject altarObject;

    [Header("인벤토리")]
    [SerializeField] private InventoryTest inventory;

    [Header("제사상 수치")]
    [SerializeField] private int requiredAmount = 100;
    [SerializeField] private int foodValue = 20;

    private int currentAmount = 0;
    private bool isCompleted = false;

    private void Start()
    {
        if (altarPanel != null)
        {
            altarPanel.SetActive(false);
        }

        if (altarConfirmPanel != null)
        {
            altarConfirmPanel.SetActive(false);
        }

        if (altarGauge != null)
        {
            altarGauge.minValue = 0;
            altarGauge.maxValue = requiredAmount;
            altarGauge.value = currentAmount;
        }
    }

    public void OpenAltar()
    {
        if (isCompleted)
        {
            Debug.Log("Altar Already Completed");
            return;
        }

        if (altarPanel != null)
        {
            altarPanel.SetActive(true);
        }

        Debug.Log("Altar Open");
    }

    public void CloseAltar()
    {
        if (altarConfirmPanel != null)
        {
            altarConfirmPanel.SetActive(false);
        }

        if (altarPanel != null)
        {
            altarPanel.SetActive(false);
        }

        Debug.Log("Altar Close");
    }

    public void TryOfferFood()
    {
        if (inventory == null)
        {
            return;
        }

        if (inventory.GetItemCount("Food") <= 0)
        {
            Debug.Log("No Food");
            return;
        }

        if (altarConfirmPanel != null)
        {
            altarConfirmPanel.SetActive(true);
        }

        Debug.Log("Offer Food?");
    }

    public void CancelOffer()
    {
        if (altarConfirmPanel != null)
        {
            altarConfirmPanel.SetActive(false);
        }

        Debug.Log("Offer Cancelled");
    }

    public void ConfirmOffer()
    {
        if (inventory == null)
        {
            return;
        }

        if (inventory.GetItemCount("Food") <= 0)
        {
            Debug.Log("No Food");

            if (altarConfirmPanel != null)
            {
                altarConfirmPanel.SetActive(false);
            }

            return;
        }

        bool removed =
            inventory.RemoveItem("Food", 1);

        if (!removed)
        {
            return;
        }

        currentAmount += foodValue;

        if (currentAmount > requiredAmount)
        {
            currentAmount = requiredAmount;
        }

        if (altarGauge != null)
        {
            altarGauge.value = currentAmount;
        }

        if (altarConfirmPanel != null)
        {
            altarConfirmPanel.SetActive(false);
        }

        Debug.Log(
            "Food Offered: " +
            currentAmount +
            " / " +
            requiredAmount
        );

        CheckComplete();
    }

    private void CheckComplete()
    {
        if (currentAmount < requiredAmount)
        {
            return;
        }

        isCompleted = true;

        Debug.Log("Altar Complete");

        if (altarPanel != null)
        {
            altarPanel.SetActive(false);
        }

        if (altarObject != null)
        {
            altarObject.SetActive(false);
        }
    }
}