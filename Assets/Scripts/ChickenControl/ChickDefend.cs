using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickDefend : DefendBase
{
    
    public override void TakeDamage(float damage)
    {
        Chicken.Instance.OnTakeDamage((int)damage);
    }
}
