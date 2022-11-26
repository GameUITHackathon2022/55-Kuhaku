using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : DefaultProjectlie
{
    public override void DoOnHit()
    {
        base.DoOnHit();
    }

    public override void DmgUser()
    {
        base.DmgUser();
    }

    protected override void Update()
    {
        //base.Update();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        float range = weaponBase.rangeDmg;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
        foreach(var col in colliders)
        {
            if(col.gameObject.CompareTag(Enemy.EnemyDefine.playerTag))
            {
                PlayerManager.Instance.UserData.SetDmg(GetDmg());
            }
        }
        Destroy(this.gameObject);

    }
}
