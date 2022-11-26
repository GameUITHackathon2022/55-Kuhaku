using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    private Tweener tween;
    public virtual void SetStat(float range)
    {

    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

    }

    public virtual void DoOnHit() { }
}
