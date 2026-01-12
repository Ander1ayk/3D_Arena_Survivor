using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private UpgradeCardUI upgradeCardPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        CreateCard(UpgradeType.Health);
        CreateCard(UpgradeType.Mana);
        CreateCard(UpgradeType.Damage);
        CreateCard(UpgradeType.Speed);
        CreateCard(UpgradeType.FireRate);
    }

    private void CreateCard(UpgradeType type)
    {
        UpgradeCardUI card = Instantiate(upgradeCardPrefab, contentParent);
        card.upgradeType = type;
    }
}
