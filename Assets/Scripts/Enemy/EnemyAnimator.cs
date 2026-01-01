using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator enemyAnimator;
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    public void PlayAttackAnimation()
    {
        enemyAnimator.SetTrigger("IsAttack");
    }
    public void PlayVictoryAnimation()
    {
        enemyAnimator.SetTrigger("IsVictory");
    }
    public void PlayDeathAnimation()
    {
        enemyAnimator.SetTrigger("IsDead");
    }
    public float GetAttackAnimationLength()
    {
        float defaultLength = 1.0f;
        if (enemyAnimator != null || enemyAnimator.runtimeAnimatorController != null)
        {
            AnimationClip[] clips = enemyAnimator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == "Attack" || clip.name == "Attacking" || clip.name == "Attack01")
                {
                    return clip.length;
                }
            }
        }
        return defaultLength;
    }
    public float GetDeathAnimationLength()
    {
        float defaultLength = 1.0f;
        if(enemyAnimator != null || enemyAnimator.runtimeAnimatorController != null)
        {
            AnimationClip[] clips = enemyAnimator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips){
                if(clip.name == "Death" || clip.name == "Die" || clip.name == "Dead")
                {
                    return clip.length;
                }
            }
        }
        return defaultLength;
    }
}
