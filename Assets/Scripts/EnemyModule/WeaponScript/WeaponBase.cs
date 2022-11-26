using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponStatus weaponBase;
    [SerializeField] protected EnemyBase enemyBase;
    public virtual void SetHolder(EnemyBase enemy) => enemyBase = enemy;

    public virtual TypeWeapon TypeWeapon => TypeWeapon.Default;
    public virtual float GetDmg() {
        return weaponBase.dmg;
    }
    public float GetRangeAffect => weaponBase.rangeDmg;
    
    public virtual void DmgUser()
    {
        enemyBase.ResetStat();
    }

}

namespace Enemy
{
    public enum TypeWeapon
    {
        Default,
        ActiveWeapon,
        RangeWeapon,
        MeleeWeapon,
    }

    [System.Serializable]
    public class WeaponStatus
    {
        public float dmg;
        public float rangeDmg;
    }
}
