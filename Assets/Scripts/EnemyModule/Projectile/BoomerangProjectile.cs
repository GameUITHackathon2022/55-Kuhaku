using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;
using Unity.VisualScripting;

public class BoomerangProjectile : DefaultProjectlie
{
    #region Boomerang Handler
    public override void SetHolder(EnemyBase enemy)
    {
        base.SetHolder(enemy);
    }

    #endregion

    public override void DmgUser()
    {
        base.DmgUser();
    }

    #region Override Handler
    protected override void OnTriggerEnter(Collider collider)
    {
        //base.OnTriggerEnter(collision);
        if (collider.gameObject.CompareTag(EnemyDefine.playerTag))
        {
            PlayerManager.Instance.UserData.SetDmg(GetDmg());
            
        }

        if(collider.gameObject.GetInstanceID() == this.GetInstanceID())
        {
            //this.gameObject.SetActive(false);   
            var _wp = weaponBelong as BoomerangWeapon; 
            if(_wp)
            {
                _wp.ResetStat();
            }
        }

        if(collider.gameObject.CompareTag(EnemyDefine.terrainTag))
        {
            Debug.Log("SOLO");
            DoOnHit();
        }
    }

    private Tween tweener;
    private bool isForward;
    public override void SetStat(float rangeMulti)
    {
        Vector3 target = enemyBase.transform.forward;
        isForward = true;
        //SetDirection(Vector3.zero);
        Debug.Log($"Do {target}");
        tweener = transform.DOMove(target * rangeMulti, 1.2f)
            .OnComplete(() =>
            {
                isForward = false;
            
            }
            );
    }

    protected override void Update()
    {
        if(isForward)
        {
            return;
        }
        //base.Update();
        transform.position = Vector3.Lerp(transform.position, enemyBase.transform.position, Time.deltaTime);
    }

    public override void DoOnHit()
    {
        if(tweener != null)
        {
            tweener.Kill();
            isForward = false;
        }
    }
    #endregion
}
