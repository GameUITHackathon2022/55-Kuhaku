using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreateManager : MonoSingleton<MapCreateManager>
{
    public int currentLevelIndex;

    public int CurrentLevel => currentLevelIndex + 1;

    public List<MapHandler> mapHandlers;

    private MapHandler currentMapHandler;

    public MapHandler CurrentMapHandler
    {
        get
        {
            if (currentMapHandler == null && currentLevelIndex >= 0 && currentLevelIndex < mapHandlers.Count)
            {
                currentMapHandler = Instantiate(mapHandlers[currentLevelIndex], this.transform);
            }
            return currentMapHandler;
        }
    }

    private void Awake()
    {
        Debug.Log("Temp create map");
        currentMapHandler = Instantiate(mapHandlers[currentLevelIndex], this.transform);
    }
}
