using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public EnemyData enemyData;
    private int currentHealth;
    [Header("Death Settings")]
    private bool isDead = false;

    private EnemyAnimator enemyAnimator;
    private WaveManager waveManager;
    private PlayerStats playerStats;

    public event Action<int, int> OnHealthChanged;

    private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
        playerStats = FindAnyObjectByType<PlayerStats>();
        currentHealth = enemyData.maxHealth;
        enemyAnimator = GetComponent<EnemyAnimator>();
        OnHealthChanged?.Invoke(currentHealth,enemyData.maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth,0, enemyData.maxHealth);
        OnHealthChanged?.Invoke(currentHealth, enemyData.maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if(isDead) return;
        isDead = true;

        if(playerStats != null)
        {
            playerStats.RecoveryMana(enemyData.manaReward);
            playerStats.EarnMoney(enemyData.coinReward);
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        waveManager.EnemyDied();
        Destroy(gameObject, enemyAnimator.GetDeathAnimationLength());
    }
}
