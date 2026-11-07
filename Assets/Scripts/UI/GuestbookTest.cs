using UnityEngine;

public class GuestbookTest : MonoBehaviour
{
    [Header("방명록 UI")]
    [SerializeField] private GameObject guestbookTestPanel;

    [Header("방명록 내용")]
    [SerializeField] private GameObject enemyContentPanel;
    [SerializeField] private GameObject itemContentPanel;
    [SerializeField] private GameObject endingContentPanel;

    [Header("적 잠금")]
    [SerializeField] private GameObject enemy1Locked;
    [SerializeField] private GameObject enemy2Locked;

    [Header("아이템 잠금")]
    [SerializeField] private GameObject item1Locked;
    [SerializeField] private GameObject item2Locked;

    [Header("엔딩 잠금")]
    [SerializeField] private GameObject ending1Locked;
    [SerializeField] private GameObject ending2Locked;

    private void Start()
    {
        if (guestbookTestPanel != null)
        {
            guestbookTestPanel.SetActive(false);
        }

        CloseAllContentPanels();
        UpdateGuestbook();
    }

    public void OpenGuestbook()
    {
        if (guestbookTestPanel != null)
        {
            guestbookTestPanel.SetActive(true);
        }

        UpdateGuestbook();
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

    public void DiscoverEnemy1()
    {
        SaveDiscovery("Enemy1");
    }

    public void DiscoverEnemy2()
    {
        SaveDiscovery("Enemy2");
    }

    public void DiscoverItem1()
    {
        SaveDiscovery("Item1");
    }

    public void DiscoverItem2()
    {
        SaveDiscovery("Item2");
    }

    public void DiscoverEnding1()
    {
        SaveDiscovery("Ending1");
    }

    public void DiscoverEnding2()
    {
        SaveDiscovery("Ending2");
    }

    private void SaveDiscovery(string key)
    {
        PlayerPrefs.SetInt("Guestbook_" + key, 1);
        PlayerPrefs.Save();

        Debug.Log("Discovered: " + key);

        UpdateGuestbook();
    }

    private void UpdateGuestbook()
    {
        SetLocked(
            enemy1Locked,
            PlayerPrefs.GetInt("Guestbook_Enemy1", 0) == 0
        );

        SetLocked(
            enemy2Locked,
            PlayerPrefs.GetInt("Guestbook_Enemy2", 0) == 0
        );

        SetLocked(
            item1Locked,
            PlayerPrefs.GetInt("Guestbook_Item1", 0) == 0
        );

        SetLocked(
            item2Locked,
            PlayerPrefs.GetInt("Guestbook_Item2", 0) == 0
        );

        SetLocked(
            ending1Locked,
            PlayerPrefs.GetInt("Guestbook_Ending1", 0) == 0
        );

        SetLocked(
            ending2Locked,
            PlayerPrefs.GetInt("Guestbook_Ending2", 0) == 0
        );
    }

    private void SetLocked(GameObject lockedObject, bool isLocked)
    {
        if (lockedObject != null)
        {
            lockedObject.SetActive(isLocked);
        }
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