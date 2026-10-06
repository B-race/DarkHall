using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SurveyHoldTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private float holdDuration = 5f;
    [SerializeField] private CollectionPanelTest collectionPanelManager;

    private float holdTime = 0f;
    private bool isHolding = false;
    private bool isCompleted = false;

    private void Start()
    {
        progressSlider.minValue = 0f;
        progressSlider.maxValue = holdDuration;
        progressSlider.value = 0f;
        progressSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isHolding && !isCompleted)
        {
            holdTime += Time.deltaTime;
            progressSlider.value = holdTime;

            if (holdTime >= holdDuration)
            {
                CompleteSurvey();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 이미 조사 완료한 장소
        if (isCompleted)
        {
            // 남은 아이템이 있을 때만 바로 수집창 다시 열기
            if (collectionPanelManager.HasRemainingItems())
            {
                collectionPanelManager.OpenCollectionPanel();
            }
            else
            {
                Debug.Log("Collection Complete");
            }

            return;
        }

        // 처음 조사하는 경우
        holdTime = 0f;
        progressSlider.value = 0f;
        progressSlider.gameObject.SetActive(true);
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isCompleted)
        {
            return;
        }

        isHolding = false;
        ResetSurvey();
    }

    private void CompleteSurvey()
    {
        holdTime = holdDuration;
        progressSlider.value = holdDuration;
        isHolding = false;
        isCompleted = true;

        progressSlider.gameObject.SetActive(false);

        Debug.Log("Survey Complete");

        collectionPanelManager.OpenCollectionPanel();
    }

    private void ResetSurvey()
    {
        holdTime = 0f;
        progressSlider.value = 0f;
        progressSlider.gameObject.SetActive(false);
    }
}