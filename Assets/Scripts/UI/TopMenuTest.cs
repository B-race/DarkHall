using UnityEngine;

public class TopMenuTest : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        CloseAllPanels();
    }

    public void OpenMap()
    {
        CloseAllPanels();
        mapPanel.SetActive(true);
    }

    public void OpenInventory()
    {
        CloseAllPanels();
        inventoryPanel.SetActive(true);
    }

    public void OpenStatus()
    {
        CloseAllPanels();
        statusPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllPanels();
        settingsPanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        mapPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        statusPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
}