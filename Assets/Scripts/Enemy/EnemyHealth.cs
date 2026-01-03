using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public EnemyData enemyData;
    private float currentHealth;
    [Header("Death Settings")]
    private bool isDead = false;

    private EnemyAnimator enemyAnimator;
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
        currentHealth = enemyData.maxHealth;
        enemyAnimator = GetComponent<EnemyAnimator>();
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        waveManager.EnemyDied();
        Destroy(gameObject, enemyAnimator.GetDeathAnimationLength());
    }
}
