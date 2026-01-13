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
    [SerializeField] private float moveSpeed = 5f;
    [Header("Multiplier")]
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float fireRateMultiplier = 1.0f;
    [Header("Upgrade Levels")]
    [SerializeField] private int healthLevel = 0;
    [SerializeField] private int manaLevel = 0;
    [SerializeField] private int damageLevel = 0;
    [SerializeField] private int speedLevel = 0;
    [SerializeField] private int fireRateLevel = 0;
    [Header("SFX")]
    [SerializeField] private AudioClip audioClipTakeDamage;
    [SerializeField] private AudioClip audioClipHeal;
    [SerializeField] private AudioClip audioClipLowHp;

    public event Action<int, int> OnHealthChanged;
    public event Action<int, int> OnManaChanged;

    private float lastHealTime;
    private bool IsDead = false;
    private PlayerAnimator playerAnimator;
    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnManaChanged?.Invoke(currentMana, maxMana);
    }
    private void Awake()
    {
        LoadStats();
        var data = PlayerProgressSaveService.LoadPlayerProgress();
        if(data != null)
        {
            maxHealth = data.maxHealth;
            maxMana = data.maxMana;
            moveSpeed = data.moveSpeed;
            damageMultiplier = data.damageMultiplier;
            fireRateMultiplier = data.fireRateMultiplier;
        }
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    private void Update()
    {
        if(currentHealth <= maxHealth * 0.2f && currentHealth > 0)
        {
            AudioManager.Instance.PlaySFX(audioClipLowHp, false, 0.9f);
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
    private void Die()
    {
        IsDead = true;
        playerAnimator.PlayerDie();
    }

    public void IncreaseMaxHealth()
    {
        healthLevel++;
        maxHealth += 10;
        SaveProgress();
        SaveStats();
    }
    public int GetHealthLevel() => healthLevel;
    public void IncreaseMaxMana()
    {
        manaLevel++;
        maxMana += 10;
        SaveProgress();
        SaveStats();
    }
    public int GetManaLevel() => manaLevel;
    public void IncreaseDamageMultiplier()
    {
        damageLevel++;
        damageMultiplier *= 1.15f;
        SaveProgress();
        SaveStats();
    }
    public int GetDamageLevel() => damageLevel;
    public void IncreaseFireRateMultiplier()
    {
        fireRateLevel++;
        fireRateMultiplier += 0.1f;
        SaveProgress();
        SaveStats();
    }
    public int GetFireRateLevel() => fireRateLevel;
    public void IncreaseSpeed()
    {
        speedLevel++;
        moveSpeed = Mathf.Min(10f, moveSpeed + 0.2f);
        SaveProgress();
        SaveStats();
    }
    public int GetSpeedLevel() => speedLevel;
    public bool GetPlayerIsDead() => IsDead;
    public float GetMoveSpeed() => moveSpeed;
    public float GetDamageMultiplier() => damageMultiplier;
    public float GetFireRateMultiplier() => fireRateMultiplier;
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public int GetMaxMana() => maxMana;
    public PlayerProgressData GetProgressData()
    {
        return new PlayerProgressData
        {
            maxHealth = maxHealth,
            maxMana = maxMana,
            moveSpeed = moveSpeed,
            damageMultiplier = damageMultiplier,
            fireRateMultiplier = fireRateMultiplier
        };
    }
    public void SaveProgress()
    {
        PlayerProgressSaveService.SavePlayerProgress(GetProgressData());
    }
    private void LoadStats()
    {
        PlayerStatsSaveData data = PlayerStatsSaveService.LoadPlayerStats();

        if(data == null) return;

        healthLevel = data.healthLevel;
        manaLevel = data.manaLevel;
        damageLevel = data.damageLevel;
        speedLevel = data.speedLevel;
        fireRateLevel = data.fireRateLevel;

        maxHealth += healthLevel * 10;
        maxMana += manaLevel * 10;
        damageMultiplier = 1f + damageLevel * 0.15f;
        moveSpeed = 2.2f + speedLevel * 0.2f;
    }
    private void SaveStats()
    {
        PlayerStatsSaveData data = new PlayerStatsSaveData
        {
            healthLevel = healthLevel,
            manaLevel = manaLevel,
            damageLevel = damageLevel,
            speedLevel = speedLevel,
            fireRateLevel = fireRateLevel
        };
        PlayerStatsSaveService.SavePlayerStats(data);
    }
    public void Upgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Health:
                IncreaseMaxHealth();
                break;
            case UpgradeType.Mana:
                IncreaseMaxMana();
                break;
            case UpgradeType.Damage:
                IncreaseDamageMultiplier();
                break;
            case UpgradeType.Speed:
                IncreaseSpeed();
                break;
            case UpgradeType.FireRate:
                IncreaseFireRateMultiplier();
                break;
        }
    }
    public int GetUpgradeLevel(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Health => healthLevel,
            UpgradeType.Mana => manaLevel,
            UpgradeType.Damage => damageLevel,
            UpgradeType.Speed => speedLevel,
            UpgradeType.FireRate => fireRateLevel,
            _ => 0
        };
    }

}
