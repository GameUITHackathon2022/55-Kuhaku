using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyBase
{
    public override EnemyType EnemyType => EnemyType.Ranged;
    protected override void OnChasePlayer()
    {
        base.OnChasePlayer();
    }

    public override void OnDoAttack()
    {
        base.OnDoAttack();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override bool InDistance()
    {
        var dis = Vector3.Distance(player.position, transform.position);
        return dis <= enemyStatus.rangeAttack * 0.7f;
        
    }
}
