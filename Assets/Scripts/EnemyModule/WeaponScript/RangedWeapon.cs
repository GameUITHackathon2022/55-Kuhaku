using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon attack in ranged
public class RangedWeapon : WeaponBase
{
    [SerializeField] Transform placeToShoot;
    public ActiveWeapon activeWeaponPrefab;
    public override float GetDmg()
    {
        return activeWeaponPrefab.GetDmg();
    }

    public override void DmgUser()
    {
        var prj = Instantiate(activeWeaponPrefab, placeToShoot.position, Quaternion.identity);
        prj.SetDirection(enemyBase.Target.position - enemyBase.transform.position);
    }
}
