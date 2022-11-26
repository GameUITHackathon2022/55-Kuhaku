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
    /// <summary>
    /// level tính từ 0
    /// Khi đem lên UI thì cộng 1
    /// </summary>
    public int level;

    public int currentHp;

    public int maxHp;

    public virtual ObjectTypeID ObjectTypeID => ObjectTypeID.NONE;

    public virtual SpecificObjectID SpecificObjectID => SpecificObjectID.NONE;

    public virtual int LEVEL
    {
        get => this.level;
        set => this.level = value;
    }
    public virtual void OnDead()
    {

    }
}
