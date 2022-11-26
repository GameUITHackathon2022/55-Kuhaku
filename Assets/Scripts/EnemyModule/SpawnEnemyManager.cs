using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemyManager : MonoSingleton<SpawnEnemyManager>
{
    [SerializeField] List<EnemyBase> currentEnemies = new List<EnemyBase>();
    public UnityAction onCompleteList;

    private void Start()
    {
        
    }

    public void AssignFunc(UnityAction unityAction)
    {
        onCompleteList += unityAction;
    }

    private void AssignEnemyToList(EnemyBase enemy)
    {
        currentEnemies.Add(enemy);
    }

    public void UnAssignEnemy(EnemyBase enemy)
    {
        currentEnemies.Remove(enemy);
        if(currentEnemies.Count <= 0)
        {
            OnEndList();
        }
    }

    public void OnEndList()
    {
        onCompleteList?.Invoke();
    }

    private void OnValidate()
    {
        currentEnemies.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy.EnemyDefine.enemyTag);
        
        foreach(var enemi in enemies)
        {
            var enemBase = enemi.GetComponent<EnemyBase>();
            if(enemBase)
            {
                AssignEnemyToList(enemBase);
            }

        }
    }
}
