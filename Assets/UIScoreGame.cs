using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreGame : MonoSingleton<UIScoreGame>
{
    [SerializeField] TextMeshProUGUI currentEnemies;
    [SerializeField] TextMeshProUGUI currentTreeLeft;

    public void SetCurrentEnemies()
    {
        currentEnemies.text = $"{SpawnEnemyManager.Instance.currentEnemy} / {SpawnEnemyManager.Instance.TotalEnemy} Enemies";
    }

    public void SetCurrentTree()
    {
        currentTreeLeft.text = $"{GroundHandler.Instance.TreeLenght} / {GameLevel.Instance.numTree} Trees";
    }
}
