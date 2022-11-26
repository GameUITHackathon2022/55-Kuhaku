using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoSingleton<VFXManager>
{
    [SerializeField] GameObject hitPlayerbyRangedFX;
    [SerializeField] GameObject hitPlayerbyMeleeFX;

    [SerializeField] GameObject playerHitEnemy;
    [SerializeField] GameObject spawnFX;
    [SerializeField] GameObject treeCollapse;
    [SerializeField] GameObject FXSpawnChicken;
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
    
    public void PlaySpawnChicken(Vector3 whoEnemy)
    {
        Instantiate(FXSpawnChicken,whoEnemy,Quaternion.identity);
    }


    public void SpawnFX(Vector3 pos)
    {
        Instantiate(spawnFX, pos, spawnFX.transform.rotation);
    }

    public void SpawnTreeCollapseFX(Vector3 pos)
    {
        Instantiate(treeCollapse, pos, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnFX(Vector3.zero);
            //new Vector3(Random.Range(-5, 5), Random.Range(0, 2), Random.Range(-5, 5)));
        }
    }
}
