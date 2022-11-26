using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickDefend : DefendBase
{
    
    public override void TakeDamage(int damage)
    {
        Chicken.Instance.OnTakeDamage(damage);
    }
}
