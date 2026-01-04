using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]private EnemyHealth enemyHealth;
    [Header("UI Bar")]
    [SerializeField] private Image hpBar;
    private float fillSpeed = 0.5f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        if(enemyHealth != null)
        enemyHealth.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDisable()
    {
        if(enemyHealth != null)
        enemyHealth.OnHealthChanged -= UpdateHealthBar;
    }
    private void UpdateHealthBar(int current, int max)
    {
        float targetFillAmount = (float) current / max;
        hpBar.DOKill();
        hpBar.DOFillAmount(targetFillAmount, fillSpeed);
    }
    private void LateUpdate()
    {
        if(mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation 
                * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}
