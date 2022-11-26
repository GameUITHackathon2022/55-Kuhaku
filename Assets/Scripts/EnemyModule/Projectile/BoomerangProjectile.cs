using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;
using Unity.VisualScripting;

public class BoomerangProjectile : DefaultProjectlie
{
    [SerializeField] float timeFloating;
    private float timeMovingFoward;
    #region Boomerang Handler
    public override void SetHolder(EnemyBase enemy)
    {
        base.SetHolder(enemy);
    }

    public void DoAttackTarget(Transform target)
    {
        
    }

    public override void SetParentWeapon(RangedWeapon weaponBase)
    {
        base.SetParentWeapon(weaponBase);
    }

    private void ShootForward(Transform Target)
    {
        if(enemyBase == null)
        {
            Debug.LogError("You dont set EnemyBase");
            this.gameObject.SetActive(false);
            return;
        }

        Vector3 dirToTarget = (Target.position - base.enemyBase.transform.position);
        Vector3 realDir = new Vector3(dirToTarget.x, 0, dirToTarget.z);

        timeMovingFoward = timeFloating;

        SetDirection(realDir);
    }

    private void ShootBackward()
    {
        if (enemyBase == null)
        {
            Debug.LogError("You dont set EnemyBase");
            this.gameObject.SetActive(false);
            return;
        }

    }

    #endregion

    #region Override Handler
    protected override void OnTriggerEnter(Collider collision)
    {
        //base.OnTriggerEnter(collision);
        if (collision.gameObject.CompareTag(EnemyDefine.playerTag))
        {
            PlayerManager.Instance.UserData.SetDmg(GetDmg());
            
        }

        if(collision.gameObject.GetInstanceID() == this.GetInstanceID())
        {
            //this.gameObject.SetActive(false);   
            var _wp = weaponBelong as BoomerangWeapon; 
            if(_wp)
            {
                _wp.ResetStat();
            }
        }

        if(collision.gameObject.CompareTag(EnemyDefine.terrainTag))
        {
            DoOnHit();
        }
    }

   
    public override void SetStat()
    {
        ShootForward(enemyBase.Target);
    }

    protected override void Update()
    {
        timeMovingFoward -= Time.deltaTime;
        if(timeMovingFoward < 0)
        {
            DoOnHit();
        }
        base.Update();
    }

    public override void DoOnHit()
    {
        //base.DoOnHit();
        Debug.Log("Do Backward");
        ShootBackward();
    }
    #endregion
}
