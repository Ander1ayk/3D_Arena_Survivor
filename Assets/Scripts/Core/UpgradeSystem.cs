using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private const int BASE_PRICE = 40;
    public void UpgradeHealth(PlayerStats playerStats)
    {
        int currentLevel = playerStats.GetHealthLevel();
        int price = BASE_PRICE * (currentLevel + 1);
        if (Wallet.Instance.SpendCoins(price))
        {
            playerStats.IncreaseMaxHealth();
            Debug.Log("Health upgraded to level " + playerStats.GetHealthLevel());
        }
        else
        {
            Debug.Log("Not enough coins to upgrade health.");
        }
    }
    public void UpgradeMana(PlayerStats playerStats)
    {
        int currentLevel = playerStats.GetManaLevel();
        int price = BASE_PRICE * (currentLevel + 1);
        if (Wallet.Instance.SpendCoins(price))
        {
            playerStats.IncreaseMaxMana();
            Debug.Log("Mana upgraded to level " + playerStats.GetManaLevel());
        }
        else
        {
            Debug.Log("Not enough coins to upgrade mana.");
        }
    }
    public void UpgradeDamageMultiplier(PlayerStats playerStats)
    {
        int currentLevel = playerStats.GetDamageLevel();
        int price = BASE_PRICE * (currentLevel + 1);
        if (Wallet.Instance.SpendCoins(price))
        {
            playerStats.IncreaseDamageMultiplier();
            Debug.Log("Damage Multiplier upgraded to level " + playerStats.GetDamageLevel());
        }
        else
        {
            Debug.Log("Not enough coins to upgrade damage multiplier.");
        }
    }
    public void UpgradeSpeed(PlayerStats playerStats)
    {
        int currentLevel = playerStats.GetSpeedLevel();
        int price = BASE_PRICE * (currentLevel + 1);
        if (Wallet.Instance.SpendCoins(price))
        {
            playerStats.IncreaseSpeed();
            Debug.Log("Speed upgraded to level " + playerStats.GetSpeedLevel());
        }
        else
        {
            Debug.Log("Not enough coins to upgrade speed.");
        }
    }
    public void UpgradeFireRateMultiplier(PlayerStats playerStats)
    {
        int currentLevel = playerStats.GetFireRateLevel();
        int price = BASE_PRICE * (currentLevel + 1);
        if (Wallet.Instance.SpendCoins(price))
        {
            playerStats.IncreaseFireRateMultiplier();
            Debug.Log("Fire Rate Multiplier upgraded to level " + playerStats.GetFireRateLevel());
        }
        else
        {
            Debug.Log("Not enough coins to upgrade fire rate multiplier.");
        }
    }
}
