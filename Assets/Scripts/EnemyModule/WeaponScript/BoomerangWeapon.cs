using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangWeapon : RangedWeapon
{
    private ActiveWeapon activeWeapon;

    private ActiveWeapon boomerangWeapon 
    {
        get
        {
            if(activeWeapon == null)
            {
                activeWeapon = Instantiate(activeWeaponPrefab, base.enemyBase.transform.position, Quaternion.identity);
                activeWeapon.SetHolder(enemyBase);
                activeWeapon.SetParentWeapon(this);
                activeWeapon.gameObject.SetActive(false);
            }

            return activeWeapon;
        }
    }

    public override void DmgUser()
    {
        if(boomerangWeapon != null)
        {
            boomerangWeapon.gameObject.SetActive(true);
            boomerangWeapon.SetStat();
        }
    }

    public void ResetStat()
    {
        boomerangWeapon.gameObject.SetActive(false);
        StartCoroutine(StartToAttack());
    }

    private IEnumerator StartToAttack()
    {
        yield return new WaitForSeconds(enemyBase.GetCoolDown);
        boomerangWeapon.SetStat();
    }
}
