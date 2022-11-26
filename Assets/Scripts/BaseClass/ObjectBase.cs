using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypeID
{
    NONE = -1,
    ATTACK = 0,
    DEFEND = 1,
    
}

public enum SpecificObjectID
{
    NONE = -1,
    PLAYER = 0,
    ENEMY = 1,
    TREE = 2,
}

public class ObjectBase : MonoBehaviour
{
    public int level;

    public int currentHp;

    public int maxHp;

    public virtual ObjectTypeID ObjectTypeID => ObjectTypeID.NONE;

    public virtual SpecificObjectID SpecificObjectID => SpecificObjectID.NONE;
}
