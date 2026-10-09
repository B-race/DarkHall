using UnityEngine;
using TMPro;

public class TitleFlowTest : MonoBehaviour
{
    [Header("이름 입력 UI")]
    [SerializeField] private GameObject nameInputTestPanel;
    [SerializeField] private TMP_InputField nameInputField;

    private string playerName = "";

    private void Start()
    {
        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        playerName = PlayerPrefs.GetString("PlayerName", "");
    }

    public void OpenNameInputPanel()
    {
        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(true);
        }

        if (nameInputField != null)
        {
            nameInputField.text = "";
            nameInputField.ActivateInputField();
        }

        Debug.Log("Name Input Open");
    }

    public void ConfirmName()
    {
        if (nameInputField == null)
        {
            return;
        }

        string inputName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(inputName))
        {
            Debug.Log("Name is empty");
            return;
        }

        playerName = inputName;

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log("Player Name Saved: " + playerName);

        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        // 실제 게임 시작은 나중에 TitleScene 통합할 때 연결
        Debug.Log("New Game Ready");
    }

    public void CancelNameInput()
    {
        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        Debug.Log("Name Input Cancelled");
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}