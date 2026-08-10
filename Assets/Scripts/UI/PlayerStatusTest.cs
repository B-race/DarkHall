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
        hp = Mathf.Max(0, hp - 10);
        UpdateUI();
    }

    public void DamageSanity()
    {
        sanity = Mathf.Max(0, sanity - 10);
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpSlider.value = hp;
        sanitySlider.value = sanity;
    }
}
