using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public void OnNextMap()
    {
        UINextmapManager.Instance.ActiveNextMap();
    }

    public void OnEndGame()
    {
        Debug.Log("Game over");
    }

    
}
