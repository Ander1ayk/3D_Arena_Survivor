using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public EnemyData enemyData;
    private Rigidbody rb;
    [Header("Target Settings")]
    [SerializeField] private Transform target;
    PlayerStats playerStats;
    EnemyAnimator enemyAnimator;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            playerStats = playerObj.GetComponent<PlayerStats>();
        }
        enemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<EnemyAnimator>();
    }
    private void FixedUpdate()
    {
        if(enemyHealth != null && enemyHealth.IsDead())
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                return;
            }
        }
        if (!target || playerStats == null) return;

        if (playerStats.GetPlayerIsDead())
        {
            enemyAnimator.PlayVictoryAnimation();
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 move = direction * enemyData.moveSpeed * Time.fixedDeltaTime;

        bool isMoving = move.magnitude > 0.01f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            if (rb != null)
            {
                rb.MovePosition(transform.position + new Vector3(move.x, 0, move.z));
            }
            else
            {
                transform.position += new Vector3(move.x,0,move.z);
            }
        }
    }
}
