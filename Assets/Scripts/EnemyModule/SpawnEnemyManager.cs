using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoSingleton<SpawnEnemyManager>
{
    [SerializeField] List<EnemyBase> enemiesPref = new List<EnemyBase>();

    public DefendBase playerBase;

    private List<EnemyBase> currentEnemies;

    private void Start()
    {
        currentEnemies = new List<EnemyBase>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetRandomEnemy();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            if(currentEnemies.Count > 0)
            {
                currentEnemies[currentEnemies.Count - 1].DestroyThisEnemy();
            }
        }
    }

    

    public EnemyBase GetRandomEnemy()
    {
        if(enemiesPref.Count == 0)
        {
            Debug.LogError("You did not assign any enemi");
        }
        int index = Random.Range(0, enemiesPref.Count);
        var enemy = Instantiate(enemiesPref[index]);
        AssignEnemyToList(enemy);
        return enemy;
    }

    private void AssignEnemyToList(EnemyBase enemy)
    {
        currentEnemies.Add(enemy);

    }

    public void UnAssignEnemy(EnemyBase enemy)
    {
        currentEnemies.Remove(enemy);
    }
}
