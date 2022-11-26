using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendBase : ObjectBase
{
    public override ObjectTypeID ObjectTypeID => ObjectTypeID.DEFEND;

    public virtual void TakeDamage(float damage)
    {

    }
}
