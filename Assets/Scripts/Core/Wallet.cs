using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance { get; private set; }
    private int coins;
    public event Action<int> OnMoneyChanged;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        coins = PlayerPrefs.GetInt("TotalMoney", 0); // Load saved money or start with 0
    }
    private void Start()
    {
        OnMoneyChanged?.Invoke(coins);
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        OnMoneyChanged?.Invoke(coins);
        PlayerPrefs.SetInt("TotalMoney", coins); // Save updated money
        PlayerPrefs.Save();
        Debug.Log("Added " + amount + " coins. Total Coins: " + coins);
    }
    public bool SpendCoins(int amount)
    {
        if(amount <= coins)
        {
            coins -= amount;
            OnMoneyChanged?.Invoke(coins);
            PlayerPrefs.SetInt("TotalMoney", coins); // Save updated money
            PlayerPrefs.Save();
            Debug.Log("Spent " + amount + " coins. Total Coins: " + coins);
            return true;
        }
        else
        {
            Debug.Log("Not enough coins to spend " + amount + ". Total Coins: " + coins);
            return false;
        }
    }
    public int GetCurrentCoins() => coins;
}
