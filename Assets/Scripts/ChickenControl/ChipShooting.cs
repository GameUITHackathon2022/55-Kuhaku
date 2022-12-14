using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChipShooting : MonoBehaviour
{

    public EnemyBase target;
    public float shootRange = 0.01f;
    
    [SerializeField]
    private float currentTime = 0;

    [SerializeField] private float range;
    
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,range);
    }

#endif
    
    private void Update()
    {
        var colliders =  Physics.OverlapSphere(this.transform.position, range, LayerMask.GetMask("Enemy"));

        target = GetTargetNearest(colliders, this.transform.position);
        if (target != null)
        {
            Vector3 dirToTarget = target.transform.position - this.transform.position;

            currentTime += Time.deltaTime;
            if (currentTime > shootRange)
            {
                SoundManager.Instance.Play("Machine",AudioType.FX,0.7f);
                GameObject t = Instantiate(GameAssets.Instance.pfBullet);
                t.transform.position = this.transform.position;
                t.transform.rotation = Quaternion.LookRotation(dirToTarget,Vector3.up);
                float time = dirToTarget.magnitude / 80;
                t.transform.DOMove(target.transform.position, time).OnComplete(() =>
                {
                    target.EnemyTakeDmg(1,null);
                    Destroy(t.gameObject);
                });
                currentTime = 0;
            }
            
            Debug.DrawRay(this.transform.position,dirToTarget,Color.green);
            dirToTarget.y = 0;
            this.transform.rotation = Quaternion.LookRotation(dirToTarget.normalized);
        }
    }

    EnemyBase GetTargetNearest(Collider[] colliders, Vector3 currentPos)
    {
        EnemyBase res = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (Vector3.Distance(currentPos, colliders[i].transform.position) <= range)
                colliders[i].TryGetComponent<EnemyBase>(out res);
        }
        return res;
    }
}
