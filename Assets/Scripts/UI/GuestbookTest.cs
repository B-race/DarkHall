using UnityEngine;

public class GuestbookTest : MonoBehaviour
{
    [Header("방명록 UI")]
    [SerializeField] private GameObject guestbookTestPanel;

    [Header("방명록 내용")]
    [SerializeField] private GameObject enemyContentPanel;
    [SerializeField] private GameObject itemContentPanel;
    [SerializeField] private GameObject endingContentPanel;

    private void Start()
    {
        if (guestbookTestPanel != null)
        {
            guestbookTestPanel.SetActive(false);
        }

        CloseAllContentPanels();
    }

    public void OpenGuestbook()
    {
        if (guestbookTestPanel != null)
        {
            guestbookTestPanel.SetActive(true);
        }

        OpenEnemyTab();

        Debug.Log("Guestbook Open");
    }

    public void CloseGuestbook()
    {
        if (guestbookTestPanel != null)
        {
            guestbookTestPanel.SetActive(false);
        }

        Debug.Log("Guestbook Close");
    }

    public void OpenEnemyTab()
    {
        CloseAllContentPanels();

        if (enemyContentPanel != null)
        {
            enemyContentPanel.SetActive(true);
        }

        Debug.Log("Guestbook Enemy Tab");
    }

    public void OpenItemTab()
    {
        CloseAllContentPanels();

        if (itemContentPanel != null)
        {
            itemContentPanel.SetActive(true);
        }

        Debug.Log("Guestbook Item Tab");
    }

    public void OpenEndingTab()
    {
        CloseAllContentPanels();

        if (endingContentPanel != null)
        {
            endingContentPanel.SetActive(true);
        }

        Debug.Log("Guestbook Ending Tab");
    }

    private void CloseAllContentPanels()
    {
        if (enemyContentPanel != null)
        {
            enemyContentPanel.SetActive(false);
        }

        if (itemContentPanel != null)
        {
            itemContentPanel.SetActive(false);
        }

        if (endingContentPanel != null)
        {
            endingContentPanel.SetActive(false);
        }
    }
}