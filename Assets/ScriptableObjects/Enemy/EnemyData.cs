using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public float attackCooldown;
    public int manaReward;
    public int coinReward;
}
