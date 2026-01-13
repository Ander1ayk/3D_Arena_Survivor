using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;

    public void InitializeAnimator()
    {
        playerAnimator = GetComponentInChildren<Animator>();

        if (playerAnimator == null)
        {
            Debug.LogError("Animator not found after initialization!");
        }
    }
    public void PlayerMove(bool IsMoving)
    {
        playerAnimator.SetBool("IsMoving", IsMoving);
    }
    public void PlayerDie()
    {
        playerAnimator.SetTrigger("IsDead");
    }
}
