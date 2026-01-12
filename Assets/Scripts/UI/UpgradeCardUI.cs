using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    [Header("Upgrade Type")]
    public UpgradeType upgradeType;

    [Header("UI Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button upgradeButton;

    [Header("Icons")]
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Sprite manaIcon;
    [SerializeField] private Sprite damageIcon;
    [SerializeField] private Sprite speedIcon;
    [SerializeField] private Sprite fireRateIcon;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        SetupIcon();
        UpdateUI();

        upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }
    private void SetupIcon()
    {
        switch (upgradeType)
        {
            case UpgradeType.Health:
                icon.sprite = healthIcon;
                break;
            case UpgradeType.Mana:
                icon.sprite = manaIcon;
                break;
            case UpgradeType.Damage:
                icon.sprite = damageIcon;
                break;
            case UpgradeType.Speed:
                icon.sprite = speedIcon;
                break;
            case UpgradeType.FireRate:
                icon.sprite = fireRateIcon;
                break;
        }
    }
    private int GetUpgradePrice()
    {
        int level = playerStats.GetUpgradeLevel(upgradeType);
        return 40 + level * 25;
    }
    private void UpdateUI()
    {
        int level = playerStats.GetUpgradeLevel(upgradeType);
        int price = GetUpgradePrice();

        valueText.text = GetStatText();
        priceText.text = price.ToString();

        upgradeButton.interactable =
            Wallet.Instance != null &&
            Wallet.Instance.GetCurrentCoins() >= price;
    }
    private void OnUpgradeClicked()
    {
        int price = GetUpgradePrice();

        if (Wallet.Instance == null)
            return;

        if (!Wallet.Instance.SpendCoins(price))
            return;

        playerStats.Upgrade(upgradeType);
        UpdateUI();
    }
    private string GetStatText()
    {
        return upgradeType switch
        {
            UpgradeType.Health => $"HP: {playerStats.GetMaxHealth()}",
            UpgradeType.Mana => $"Mana: {playerStats.GetMaxMana()}",
            UpgradeType.Damage => $"Damage x{playerStats.GetDamageMultiplier():0.00}",
            UpgradeType.Speed => $"Speed: {playerStats.GetMoveSpeed():0.0}",
            UpgradeType.FireRate => $"Fire rate x{playerStats.GetFireRateMultiplier():0.0}",
            _ => ""
        };
    }
    private void OnEnable()
    {
        if (Wallet.Instance != null)
            Wallet.Instance.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        if (Wallet.Instance != null)
            Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        UpdateUI();
    }
}
