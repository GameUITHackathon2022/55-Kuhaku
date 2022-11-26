using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon only trigger once: projectile ... ?
public class ActiveWeapon : WeaponBase
{
    [SerializeField] RangedWeapon weaponBelong;

    private Vector3 dir;
    [SerializeField] protected float speed;
    public virtual void SetDirection(Vector3 _dir)
    {
        this.dir = _dir;
    }

    protected Vector3 Dir => dir;

    protected virtual void OnTriggerEnter(Collider collision)
    {

    }
}
