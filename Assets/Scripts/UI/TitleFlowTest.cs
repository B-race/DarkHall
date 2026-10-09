using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleFlowTest : MonoBehaviour
{
    [Header("이름 입력 UI")]
    [SerializeField] private GameObject nameInputTestPanel;
    [SerializeField] private TMP_InputField nameInputField;

    [Header("이어하기 UI")]
    [SerializeField] private GameObject continueTestPanel;
    [SerializeField] private Button saveSlot1Button;
    [SerializeField] private Button saveSlot2Button;
    [SerializeField] private Button saveSlot3Button;

    private string playerName = "";

    private void Start()
    {
        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        if (continueTestPanel != null)
        {
            continueTestPanel.SetActive(false);
        }

        playerName = PlayerPrefs.GetString("PlayerName", "");

        UpdateSaveSlotButtons();
    }

    // =========================
    // 새 게임
    // =========================

    public void OpenNameInputPanel()
    {
        if (continueTestPanel != null)
        {
            continueTestPanel.SetActive(false);
        }

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

        // 테스트용:
        // 새 게임을 만들면 첫 번째 빈 세이브 슬롯에 저장
        int emptySlot = FindEmptySaveSlot();

        if (emptySlot != -1)
        {
            PlayerPrefs.SetInt("SaveSlot" + emptySlot + "_Exists", 1);
            PlayerPrefs.SetString(
                "SaveSlot" + emptySlot + "_PlayerName",
                playerName
            );

            Debug.Log("Save Slot " + emptySlot + " Created");
        }
        else
        {
            Debug.Log("No Empty Save Slot");
        }

        PlayerPrefs.Save();

        Debug.Log("Player Name Saved: " + playerName);

        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        UpdateSaveSlotButtons();

        // 실제 게임 씬 이동은 통합할 때 연결
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

    // =========================
    // 이어하기
    // =========================

    public void OpenContinuePanel()
    {
        if (nameInputTestPanel != null)
        {
            nameInputTestPanel.SetActive(false);
        }

        if (continueTestPanel != null)
        {
            continueTestPanel.SetActive(true);
        }

        UpdateSaveSlotButtons();

        Debug.Log("Continue Panel Open");
    }

    public void CloseContinuePanel()
    {
        if (continueTestPanel != null)
        {
            continueTestPanel.SetActive(false);
        }

        Debug.Log("Continue Panel Close");
    }

    public void LoadSaveSlot1()
    {
        LoadSaveSlot(1);
    }

    public void LoadSaveSlot2()
    {
        LoadSaveSlot(2);
    }

    public void LoadSaveSlot3()
    {
        LoadSaveSlot(3);
    }

    private void LoadSaveSlot(int slotNumber)
    {
        bool exists =
            PlayerPrefs.GetInt(
                "SaveSlot" + slotNumber + "_Exists",
                0
            ) == 1;

        if (!exists)
        {
            Debug.Log("Save Slot " + slotNumber + " is Empty");
            return;
        }

        playerName = PlayerPrefs.GetString(
            "SaveSlot" + slotNumber + "_PlayerName",
            ""
        );

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log(
            "Save Slot " + slotNumber +
            " Loaded - Player: " + playerName
        );

        // 실제 게임 씬 이동은 통합할 때 연결
        Debug.Log("Continue Game Ready");
    }

    // =========================
    // 세이브 슬롯 관리
    // =========================

    private int FindEmptySaveSlot()
    {
        for (int i = 1; i <= 3; i++)
        {
            int exists =
                PlayerPrefs.GetInt(
                    "SaveSlot" + i + "_Exists",
                    0
                );

            if (exists == 0)
            {
                return i;
            }
        }

        return -1;
    }

    private void UpdateSaveSlotButtons()
    {
        if (saveSlot1Button != null)
        {
            saveSlot1Button.interactable =
                PlayerPrefs.GetInt("SaveSlot1_Exists", 0) == 1;
        }

        if (saveSlot2Button != null)
        {
            saveSlot2Button.interactable =
                PlayerPrefs.GetInt("SaveSlot2_Exists", 0) == 1;
        }

        if (saveSlot3Button != null)
        {
            saveSlot3Button.interactable =
                PlayerPrefs.GetInt("SaveSlot3_Exists", 0) == 1;
        }
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}