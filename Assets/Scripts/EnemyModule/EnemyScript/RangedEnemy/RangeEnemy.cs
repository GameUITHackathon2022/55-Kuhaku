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


}
