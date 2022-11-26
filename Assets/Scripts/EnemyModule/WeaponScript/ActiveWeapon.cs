using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon only trigger once: projectile ... ?
public class ActiveWeapon : WeaponBase
{
    [SerializeField] protected RangedWeapon weaponBelong;

    private Vector3 dir;
    [SerializeField] protected float speed;
    public virtual void SetDirection(Vector3 _dir)
    {
        this.dir = _dir;
    }

    public virtual void SetParentWeapon(RangedWeapon weaponBase)
    {
        weaponBelong = weaponBase;
    }

    protected Vector3 Dir => dir;

    public virtual void SetStat()
    {

    }

    protected virtual void OnTriggerEnter(Collider collision)
    {

    }

    public virtual void DoOnHit() { }
}
