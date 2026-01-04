using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int amountHeal = 30;
    [Header("Keys")]
    [SerializeField] private KeyCode healKey = KeyCode.Mouse0;

    private void Update()
    {
        if (Input.GetKeyDown(healKey))
        {
            playerStats.UseManaHeal(amountHeal);
        }
    }
}
