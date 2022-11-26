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
                activeWeapon = Instantiate(activeWeaponPrefab, base.enemyBase.transform);
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
            //boomerangWeapon.DmgUser();
        }
    }

    public void ResetStat()
    {
        boomerangWeapon.gameObject.SetActive(false);
        
    }

    private IEnumerator StartToAttack()
    {
        yield return new WaitForSeconds(enemyBase.GetCoolDown);

    }
}
