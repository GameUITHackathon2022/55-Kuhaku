using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ObjectStatConfigs", fileName = "ObjectStatConfigs")]
public class ObjectStatConfigs : ScriptableObject
{
    public static ObjectStatConfigs Instance => ConfigsManager.Instance.statConfigs;
 
    public ObjectHealthConfigs healthConfigs;

    internal float GetHealthBySpecificID(SpecificObjectID specificObjectID, int level)
    {
        return healthConfigs.GetHealthBySpecificID(specificObjectID, level);
    }
}

[System.Serializable]
public class ObjectHealthConfigs
{
    public List<ObjectHealth> objectHealths;

    public float GetHealthBySpecificID(SpecificObjectID _specificObjectID, int _level)
    {
        float health = -1;

        ObjectHealth objectHealth = objectHealths.Find(x => x.specificObjectID == _specificObjectID);

        health = objectHealth.GetHealthByLevel(_level);

        return health;
    }
}

[System.Serializable]
public class ObjectHealth
{
    public SpecificObjectID specificObjectID;
    public List<float> health;

    internal float GetHealthByLevel(int _level)
    {
        if (_level >= 0 && _level < health.Count)
        {
            return health[_level];
        }
        return -1;
    }
}
