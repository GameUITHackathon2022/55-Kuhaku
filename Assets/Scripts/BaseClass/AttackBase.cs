using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : ObjectBase
{
    public override ObjectTypeID ObjectTypeID => ObjectTypeID.ATTACK;

    public int damage = 0;

    public virtual void AttackDamage()
    {

    }
}
