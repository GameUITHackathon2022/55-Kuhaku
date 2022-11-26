using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemyManager : MonoSingleton<SpawnEnemyManager>
{
    [SerializeField] List<EnemyBase> currentEnemies = new List<EnemyBase>();
    public UnityAction onCompleteList;
    [SerializeField] int totalEnemy;
    public int TotalEnemy => totalEnemy;
    public int currentEnemy => currentEnemies.Count;
    private void Start()
    {
        totalEnemy = currentEnemies.Count;
        UIScoreGame.Instance.SetCurrentEnemies();
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
        UIScoreGame.Instance.SetCurrentEnemies();
        if (currentEnemies.Count <= 0)
        {
            OnEndList();
        }
    }

    public void OnEndList()
    {
        onCompleteList?.Invoke();
    }

    public void SetEnemyNumber()
    {
        currentEnemies.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy.EnemyDefine.enemyTag);

        foreach (var enemi in enemies)
        {
            var enemBase = enemi.GetComponent<EnemyBase>();
            if (enemBase)
            {
                AssignEnemyToList(enemBase);
            }

        }
    }
    private void OnValidate()
    {
        SetEnemyNumber();
    }
}
