using UnityEngine;

public class TitleReturnTest : MonoBehaviour
{
    [SerializeField] private GameObject titleConfirmPanel;

    private void Start()
    {
        titleConfirmPanel.SetActive(false);
    }

    public void OpenConfirmPanel()
    {
        titleConfirmPanel.SetActive(true);
    }

    public void CancelReturn()
    {
        titleConfirmPanel.SetActive(false);
    }

    public void ConfirmReturn()
    {
        Debug.Log("Return To Title");
        titleConfirmPanel.SetActive(false);
    }
}