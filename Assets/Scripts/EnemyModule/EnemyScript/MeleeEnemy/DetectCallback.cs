using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCallback : MonoBehaviour
{
    [SerializeField] MeleeEnemy meleeEnemy;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag(EnemyDefine.terrainTag))
        {
            meleeEnemy.OnChangeHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(EnemyDefine.playerTag))
        {
            meleeEnemy.OnDoAttack();

        }
    }
}
