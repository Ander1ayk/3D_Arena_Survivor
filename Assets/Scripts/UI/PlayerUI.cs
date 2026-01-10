using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerStats playerStats;

    [Header("UI Bars")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Gradient colorGradientHealth;
    [SerializeField] private Image manaBarFill;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private Gradient colorGradientMana;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinText;

    private float fillSpeed = 1f;
    private void OnEnable()
    {
        playerStats.OnHealthChanged += UpdateHealUI;
        playerStats.OnManaChanged += UpdateManaUI;
        if(Wallet.Instance != null)
        {
            Wallet.Instance.OnMoneyChanged += UpdateCoinUI;
            UpdateCoinUI(Wallet.Instance.GetCurrentCoins());
        }
    }
    private void OnDisable()
    {
        playerStats.OnHealthChanged -= UpdateHealUI;
        playerStats.OnManaChanged -= UpdateManaUI;
        if (Wallet.Instance != null)
        {
            Wallet.Instance.OnMoneyChanged -= UpdateCoinUI;
        }
    }

    private void UpdateHealUI(int current, int max)
    {
        float targetFillAmount = (float)current / max;
        // stop animation
        healthBarFill.DOKill();
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        healthBarFill.DOColor(colorGradientHealth.Evaluate(targetFillAmount), fillSpeed);
        healthText.text = "Health: " + current;
        Debug.Log("UI health updated");
    }
    private void UpdateManaUI(int current, int max)
    {
        float targetFillAmount = (float) current/max;
        manaBarFill.DOKill();
        manaBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        manaBarFill.DOColor(colorGradientMana.Evaluate(targetFillAmount), fillSpeed);
        manaText.text = "Mana: " + current;
        Debug.Log("UI mana updated");
    }
    private void UpdateCoinUI(int coins)
    {
        coinText.text = "Coins: "+ coins;
    }
}
