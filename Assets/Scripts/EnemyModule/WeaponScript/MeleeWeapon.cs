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
        //base.DmgUser();
        Collider[] collider = Physics.OverlapSphere(enemyBase.transform.position, enemyBase.GetRange);

        foreach(var col in collider)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.UserData.SetDmg(GetDmg());
                return;
            }
        }
    }
}

namespace Enemy
{
    
}