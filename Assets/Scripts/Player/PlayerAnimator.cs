using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
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
