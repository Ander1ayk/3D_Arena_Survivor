using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [Header("Mana")]
    [SerializeField] private int maxMana = 50;
    private int currentMana;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [Header("Multiplier")]
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private float fireRateMultiplier = 1.0f;

    private bool IsDead = false;
    private PlayerAnimator playerAnimator;
    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
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
}
