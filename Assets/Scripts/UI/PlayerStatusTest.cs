using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusTest : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider sanitySlider;

    private int hp = 100;
    private int sanity = 100;

    private void Start()
    {
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

    private void CheckGameOver()
    {
        if (hp <= 0 || sanity <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void UpdateUI()
    {
        hpSlider.value = hp;
        sanitySlider.value = sanity;
    }
}
