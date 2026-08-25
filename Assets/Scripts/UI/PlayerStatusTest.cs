using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusTest : MonoBehaviour
{
    [Header("기본 HUD")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider sanitySlider;

    [Header("상태창")]
    [SerializeField] private Slider statusHpSlider;
    [SerializeField] private Slider statusSanitySlider;

    private int hp = 100;
    private int sanity = 100;

    private void Start()
    {
        SetupSlider(hpSlider);
        SetupSlider(sanitySlider);
        SetupSlider(statusHpSlider);
        SetupSlider(statusSanitySlider);

        UpdateUI();
    }

    public void DamageHP()
    {
        hp = Mathf.Clamp(hp - 10, 0, 100);

        CheckGameOver();
        UpdateUI();
    }

    public void HealHP()
    {
        hp = Mathf.Clamp(hp + 10, 0, 100);

        UpdateUI();
    }

    public void DamageSanity()
    {
        sanity = Mathf.Clamp(sanity - 10, 0, 100);

        CheckGameOver();
        UpdateUI();
    }

    public void HealSanity()
    {
        sanity = Mathf.Clamp(sanity + 10, 0, 100);

        UpdateUI();
    }

    private void SetupSlider(Slider slider)
    {
        if (slider == null)
        {
            return;
        }

        slider.minValue = 0;
        slider.maxValue = 100;
    }

    private void CheckGameOver()
    {
        if (hp <= 0 || sanity <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = hp;
        }

        if (sanitySlider != null)
        {
            sanitySlider.value = sanity;
        }

        if (statusHpSlider != null)
        {
            statusHpSlider.value = hp;
        }

        if (statusSanitySlider != null)
        {
            statusSanitySlider.value = sanity;
        }
    }
}