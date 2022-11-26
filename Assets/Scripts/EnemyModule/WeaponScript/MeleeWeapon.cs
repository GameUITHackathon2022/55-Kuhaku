using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon attack in melee ranged
public class MeleeWeapon : WeaponBase
{
    public override TypeWeapon TypeWeapon => TypeWeapon.MeleeWeapon;

    public override void DmgUser()
    {
        if(enemyBase.Target)
        {
            enemyBase.Crr.TakeDamage(weaponBase.dmg);
           // VFXManager.Instance.PlayHitPlayerMelee(enemyBase.Crr.transform);
        }
    }
}

namespace Enemy
{
    
}