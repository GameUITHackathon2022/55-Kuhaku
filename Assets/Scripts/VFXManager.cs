using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoSingleton<VFXManager>
{
    [SerializeField] GameObject hitPlayerbyRangedFX;
    [SerializeField] GameObject hitPlayerbyMeleeFX;

    [SerializeField] GameObject playerHitEnemy;
    public void PlayHitPlayerRanged(Vector3 target)
    {
        Instantiate(hitPlayerbyRangedFX, target, Quaternion.identity);
    }

    public void PlayHitPlayerMelee(Vector3 target)
    {
        Instantiate(hitPlayerbyMeleeFX, target, Quaternion.identity);
    }

    public void PlayHitEnemy(Vector3 whoEnemy)
    {
        Instantiate(playerHitEnemy, whoEnemy, Quaternion.identity);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    PlayHitEnemy(Vector3.zero);
        //        //new Vector3(Random.Range(-5, 5), Random.Range(0, 2), Random.Range(-5, 5)));
        //}
    }
}
