using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : DefendBase
{
    public override SpecificObjectID SpecificObjectID => SpecificObjectID.TREE;
    public override int LEVEL
    {
        get => base.LEVEL;
        set
        {
            this.level = value;
            this.maxHp = ObjectStatConfigs.Instance.GetHealthBySpecificID(SpecificObjectID, level);
        }
    }

}
