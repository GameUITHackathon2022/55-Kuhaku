using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EnemyBase enemyBase;
    public void TriggerRun()
    {
        if (animator.GetBool(AnimatorEnemy.doRun))
            animator.SetBool(AnimatorEnemy.doRun, true);
    }

    public void TriggerAttack()
    {
        if (enemyBase.EnemyType == Enemy.EnemyType.Melee)
        {
            animator.SetTrigger(AnimatorEnemy.attackMelee);
        }
        else
        {
            animator.SetTrigger(AnimatorEnemy.attackRanged);
        }
    }

    public void TriggerIddle()
    {
        if(animator.GetBool(AnimatorEnemy.doRun))
            animator.SetBool(AnimatorEnemy.doRun, false);
    }

    public void DoOnAttack()
    {
        enemyBase.OnDoAttack();
    }
}

public static class AnimatorEnemy
{
    public static readonly string attackMelee = "fireRight";
    public static readonly string attackRanged = "fireLeft";
    public static readonly string doRun = "isRun";

}
