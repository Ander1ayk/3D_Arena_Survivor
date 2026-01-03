using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    private float destroyBulletTime = 3;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        Destroy(gameObject, destroyBulletTime);
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            int fullDamage = Mathf.RoundToInt( damage * playerStats.GetDamageMultiplier());
            if(other.TryGetComponent(out EnemyHealth health))
            {
                health.TakeDamage(fullDamage);
                Debug.Log("Enemy got damage " + fullDamage);
            }
            Destroy(gameObject);
        }
    }
}
