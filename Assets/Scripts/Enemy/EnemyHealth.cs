using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public EnemyData enemyData;
    private int currentHealth;
    [Header("Death Settings")]
    private bool isDead = false;

    [Header("Audio clips")]
    [SerializeField] private AudioClip audioClipTakeDamage;
    [SerializeField] private AudioClip audioClipDeath;

    private EnemyAnimator enemyAnimator;
    private WaveManager waveManager;
    private PlayerStats playerStats;

    public event Action<int, int> OnHealthChanged;
    private int maxHealthScaled;

    private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
        playerStats = FindAnyObjectByType<PlayerStats>();
        enemyAnimator = GetComponent<EnemyAnimator>();

        float multiplier = waveManager.GetDifficultyMultiplier();

        maxHealthScaled = Mathf.RoundToInt(enemyData.maxHealth * multiplier);
        currentHealth = maxHealthScaled;

        OnHealthChanged?.Invoke(currentHealth, maxHealthScaled);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth,0, maxHealthScaled);
        OnHealthChanged?.Invoke(currentHealth, maxHealthScaled);

        AudioManager.Instance.PlaySFX(audioClipTakeDamage, true, 0.8f);

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if(isDead) return;
        isDead = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (playerStats != null)
        {
            playerStats.RecoveryMana(enemyData.manaReward);
            if(Wallet.Instance != null)
            {
                Wallet.Instance.AddCoins(enemyData.coinReward);
            }
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        AudioManager.Instance.PlaySFX(audioClipDeath, true, 0.7f);

        waveManager.EnemyDied();
        enemyAnimator.PlayDeathAnimation();
        Destroy(gameObject, enemyAnimator.GetDeathAnimationLength());
    }
    public bool IsDead()
    {
        return isDead;
    }
}
