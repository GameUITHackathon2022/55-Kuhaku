using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BullAttack : MeleeEnemy
{
    protected override MeleeEnemyType MeleeEnemyType => MeleeEnemyType.Default;
    private bool DoWait = true;
    private Vector3 DirectionHeading; 
    #region Bull Style region
    private void DoRushInto()
    {
        float speedFrame = enemyStatus.enemySpeed;

        //transform.position = Vector3.MoveTowards(transform.position, playerPos, speedFrame);
        //transform.Translate(transform.forward * speedFrame);
        Vector3 posLead = DirectionHeading * speedFrame;
        //thisRG.MovePosition(posLead);
        thisRG.velocity = new Vector3(posLead.x , thisRG.velocity.y, posLead.z);
    }

    private IEnumerator WaitForTheNextRound(UnityAction doAfterWait)
    {
        yield return new WaitForSeconds(1.2f);
        doAfterWait?.Invoke();
    }

    protected void CalculateDir(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        DirectionHeading = new Vector3(dir.x, 0, dir.z);
    }
    #endregion

    #region Override Handler
    public override void SetStatus()
    {
        base.SetStatus();
        CalculateDir(Target);
        DoWait = false;
    }

    protected override void FixedUpdate()
    {
        if(!DoWait)
            base.FixedUpdate();
    }

    protected override void OnChasePlayer()
    {
        //base.OnChasePlayer();
        DoRushInto();
    }


    public override void OnDoAttack()
    {
        //base.OnDoAttack();
        if (CanAttack)
        {
            Debug.Log("Hi");
            weaponBase.DmgUser();
            base.ResetStat();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        if(collision.collider.CompareTag(EnemyDefine.terrainTag))
        {
            OnChangeHit();
        }
    }
    #endregion

    public override void OnChangeHit()
    {
        base.OnChangeHit();
        DoWait = true;
        thisRG.Sleep();

        StartCoroutine(WaitForTheNextRound(() =>
        {
            DoWait = false;
            CalculateDir(Target);
        }
        ));
    }
}
