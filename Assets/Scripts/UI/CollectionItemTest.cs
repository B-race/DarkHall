using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CollectionItemTest : MonoBehaviour
{
    [SerializeField] private string itemName = "Wood";
    [SerializeField] private TestItemType itemType;
    [SerializeField] private InventoryTest inventory;

    private Button itemButton;

    private void Awake()
    {
        itemButton = GetComponent<Button>();
        itemButton.onClick.AddListener(CollectItem);
    }

    private void CollectItem()
    {
        bool added = inventory.AddItem(itemName, itemType);

        if (added)
        {
            gameObject.SetActive(false);
        }
    }
}