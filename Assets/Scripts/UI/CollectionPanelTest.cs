using UnityEngine;

public class CollectionPanelTest : MonoBehaviour
{
    [SerializeField] private GameObject collectionPanel;

    private CollectionItemTest[] collectionItems;

    private void Awake()
    {
        collectionItems =
            collectionPanel.GetComponentsInChildren<CollectionItemTest>(true);
    }

    private void Start()
    {
        collectionPanel.SetActive(false);
    }

    private void Update()
    {
        // 수집창이 열려 있는데 남은 아이템이 하나도 없으면 자동으로 닫기
        if (collectionPanel.activeSelf && !HasRemainingItems())
        {
            CloseCollectionPanel();
        }
    }

    public void OpenCollectionPanel()
    {
        // 남은 아이템이 있을 때만 수집창 열기
        if (HasRemainingItems())
        {
            collectionPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Collection Complete");
        }
    }

    public void CloseCollectionPanel()
    {
        collectionPanel.SetActive(false);
    }

    public bool HasRemainingItems()
    {
        for (int i = 0; i < collectionItems.Length; i++)
        {
            if (collectionItems[i].gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}