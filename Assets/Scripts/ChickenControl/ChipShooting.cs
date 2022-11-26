using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ChipShooting : MonoBehaviour
{

    public Transform target;
    public float shootRange = 0.5f;
    private float currentTime = 0;
    private void Update()
    {
        if (target != null)
        {
            Vector3 dirToTarget = target.transform.position - this.transform.position;

            currentTime += Time.deltaTime;
            if (currentTime > shootRange)
            {
                GameObject t = Instantiate(GameAssets.Instance.pfBullet);
                t.transform.position = this.transform.position;
                t.transform.rotation = quaternion.LookRotation(dirToTarget,Vector3.up);
                t.transform.DOMove(target.transform.position, 1f).OnComplete(() =>
                {
                    Destroy(t.gameObject);
                });
                currentTime = 0;
            }
                
            Debug.DrawRay(this.transform.position,dirToTarget,Color.green);
        }
    }
}
