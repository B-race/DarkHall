using UnityEngine;

public class CollectionPanelTest : MonoBehaviour
{
    [SerializeField] private GameObject collectionPanel;

    private void Start()
    {
        collectionPanel.SetActive(false);
    }

    public void OpenCollectionPanel()
    {
        collectionPanel.SetActive(true);
    }

    public void CloseCollectionPanel()
    {
        collectionPanel.SetActive(false);
    }
}