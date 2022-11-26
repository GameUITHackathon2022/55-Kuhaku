using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

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

        Vector3 dirToBack = transform.position - base.enemyBase.transform.position;
        Vector3 realDir = new Vector3(dirToBack.x, 0, dirToBack.z);

        SetDirection(realDir);
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

        if(collision.gameObject.CompareTag(EnemyDefine.enemyTag))
        {

        }
    }

    protected override void Start()
    {
        ShootForward(enemyBase.Target);
    }

    protected override void Update()
    {
        timeMovingFoward -= Time.deltaTime;
        if(timeMovingFoward < 0)
        {
            ShootBackward();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    #endregion
}
