using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using DG.Tweening;
public class MeleeEnemy : EnemyBase
{
    #region Style Enemy Decision
    public override EnemyType EnemyType => EnemyType.Melee;

    protected virtual MeleeEnemyType MeleeEnemyType => MeleeEnemyType.Default;
  
    #endregion
    #region Base Enemy override Handler
    protected override void OnChasePlayer()
    {
        base.OnChasePlayer();
    }

    public override void OnDoAttack()
    {
        base.OnDoAttack();
    }

    public override void DestroyThisEnemy()
    {
        base.DestroyThisEnemy();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    public virtual void OnChangeHit()
    {
            
    }
}
