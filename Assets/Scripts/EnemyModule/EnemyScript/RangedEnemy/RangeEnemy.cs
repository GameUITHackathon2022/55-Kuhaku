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
        return base.InDistance();
        //Vector3 vector3Dis = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        //var dis = Vector3.Distance(vector3Dis, transform.position);
        //return dis <= enemyStatus.rangeAttack;
        
    }
}
