using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public EnemyData enemyData;
    private float lastAttackTime;
    private int scaledDamage;

    private EnemyAnimator enemyAnimator;
    private float cooldown;
    private void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();

        WaveManager waveManager = FindFirstObjectByType<WaveManager>();
        float multiplier = waveManager.GetDifficultyMultiplier();

        scaledDamage = Mathf.RoundToInt(enemyData.damage * multiplier);
    }
    private void OnTriggerStay(Collider other)
    {
        if(enemyAnimator.GetAttackAnimationLength() > enemyData.attackCooldown)
        {
            cooldown = enemyAnimator.GetAttackAnimationLength();
        }
        else
        {
            cooldown = enemyData.attackCooldown;
        }
        if (Time.time - lastAttackTime > cooldown)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
                if (player != null)
                {
                    player.TakeDamage(scaledDamage);
                    lastAttackTime = Time.time;
                    enemyAnimator.PlayAttackAnimation();
                }
            }
        }
    }
}
