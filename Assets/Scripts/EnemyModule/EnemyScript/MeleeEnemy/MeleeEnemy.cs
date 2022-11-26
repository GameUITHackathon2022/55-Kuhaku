using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using DG.Tweening;
using UnityEngine.Events;

public class MeleeEnemy : EnemyBase
{

    public int timeExplore;
    #region Style Enemy Decision
    public override EnemyType EnemyType => EnemyType.Melee;

    protected virtual MeleeEnemyType MeleeEnemyType => MeleeEnemyType.Default;
  
    #endregion
    #region Base Enemy override Handler

    public override void Start()
    {
        base.Start();
        StartCoroutine(CountDownToExplore());
    }
    
    public override void EnemyTakeDmg(int dmg, UnityAction unityAction)
    {
        crrHp -= dmg;
        float percent = (float)crrHp / enemyStatus.enemyHp;
        //enemyUI.SetFillBar(percent);
        Debug.Log(percent);
        healthBar.fillAmount = percent;
        unityAction?.Invoke();
        if(crrHp <= 0)
        {
            DestroyThisEnemy();
        }
    }

    protected override void OnChasePlayer()
    {
        //base.OnChasePlayer();
    }

    public override void OnDoAttack()
    {
        //base.OnDoAttack();
    }

    public override void DestroyThisEnemy()
    {
        //base.DestroyThisEnemy();
    }

    protected override void FixedUpdate()
    {
        //base.FixedUpdate();
    }
    #endregion

    public virtual void OnChangeHit()
    {
            
    }

    IEnumerator CountDownToExplore()
    {
        float curTime = timeExplore;
        while (curTime >= 0)
        {
            curTime -= Time.deltaTime;
            yield return null;
        }
        VFXManager.Instance.SpawnFXExplo(this.transform.position);

        var chicken = Chicken.Instance;
        if(Vector3.Distance(chicken.transform.position,new Vector3(this.transform.position.x,chicken.transform.position.y,this.transform.position.z)) <= 3)
            chicken.OnTakeDamage(10);
        Destroy(Target.gameObject);
        Destroy(this.gameObject);
    }
}
