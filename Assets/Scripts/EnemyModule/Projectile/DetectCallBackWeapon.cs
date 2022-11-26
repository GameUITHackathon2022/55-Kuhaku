using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
[RequireComponent(typeof(Collider))]
public class DetectCallBackWeapon : MonoBehaviour
{
    public EnemyBase enemiesProjectile;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag(EnemyDefine.terrainTag))
        {
            ActiveWeapon wp = enemiesProjectile.WeaponBase as ActiveWeapon;
            if(wp)
            {
                //enemiesProjectile.WeaponBase.DoOnHit();
                Debug.Log("Hello");
                wp.DoOnHit();
            }
        }
    }

}
