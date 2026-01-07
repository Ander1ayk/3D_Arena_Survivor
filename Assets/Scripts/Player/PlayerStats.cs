using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 60;
    private int currentHealth;
    [Header("Mana")]
    [SerializeField] private int maxMana = 60;
    private int currentMana;
    private float healRate = 1f;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.2f;
    [Header("Multiplier")]
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private float fireRateMultiplier = 1.0f;
    [Header("SFX")]
    [SerializeField] private AudioClip audioClipTakeDamage;
    [SerializeField] private AudioClip audioClipHeal;
    [SerializeField] private AudioClip audioClipLowHp;
    [Header("Money")]
    private int coins;

    public event Action<int, int> OnHealthChanged;
    public event Action<int, int> OnManaChanged;
    public event Action<int> OnMoneyChanged;

    private float lastHealTime;
    private bool IsDead = false;
    private PlayerAnimator playerAnimator;
    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnManaChanged?.Invoke(currentMana, maxMana);
        OnMoneyChanged?.Invoke(coins);
    }
    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    private void Update()
    {
        if(currentHealth <= maxHealth * 0.2f)
        {
            AudioManager.Instance.PlaySFX(audioClipLowHp, false, 1);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);

        AudioManager.Instance.PlaySFX(audioClipTakeDamage, false, 0.9f);

        if (currentHealth <= 0) Die();    
    }
    public void UseManaHeal(int amount)
    {
        if (Time.time - lastHealTime < healRate)
        {
            Debug.Log("Ability is on cooldown");
            return;
        }
        if (amount <= currentMana)
        {
            lastHealTime = Time.time;

            currentMana -= amount;
            currentHealth += amount / 2;

            AudioManager.Instance.PlaySFX(audioClipHeal, false, 0.9f);

            currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnManaChanged?.Invoke(currentMana, maxMana);
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }
    public void RecoveryMana(int amount)
    {
        if(IsDead) return;

        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0,maxMana);

        OnManaChanged?.Invoke(currentMana, maxMana);
        Debug.Log($"Mana recovered: {amount}. Current mana {currentMana}");
    }
    public void EarnMoney(int amount)
    {
        coins += amount;
        OnMoneyChanged?.Invoke(coins);
    }
    public bool SpendMoney(int amount)
    {
        if(amount <= coins)
        {
            coins -= amount;
            OnMoneyChanged?.Invoke(coins);
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }
    private void Die()
    {
        IsDead = true;
        playerAnimator.PlayerDie();
    }
    public bool GetPlayerIsDead()
    {
        return IsDead;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed * speedMultiplier;
    }
    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public float GetFireRateMultiplier()
    {
        return fireRateMultiplier;
    }
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
