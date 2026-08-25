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

        // 게임 시작 시 조사 게이지 숨김
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
        if (isCompleted)
        {
            return;
        }

        holdTime = 0f;
        progressSlider.value = 0f;

        // 홀드 시작 시 게이지 표시
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

        // 조사 완료 전에 손을 떼면 초기화 + 게이지 숨김
        ResetSurvey();
    }

    private void CompleteSurvey()
    {
        holdTime = holdDuration;
        progressSlider.value = holdDuration;

        isHolding = false;
        isCompleted = true;

        // 조사 완료 후 게이지 숨김
        progressSlider.gameObject.SetActive(false);

        Debug.Log("Survey Complete");

        // 수집창 출력
        collectionPanelManager.OpenCollectionPanel();
    }

    private void ResetSurvey()
    {
        holdTime = 0f;
        progressSlider.value = 0f;

        progressSlider.gameObject.SetActive(false);
    }
}