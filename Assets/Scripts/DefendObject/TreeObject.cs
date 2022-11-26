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
            this.currentHp = maxHp;
        }
    }

    private void Awake()
    {
        LEVEL = level;
    }

    public override void TakeDamage(int damage)
    {
        currentHp = maxHp - currentHp;
        if (currentHp < 0)
        { 
            currentHp = 0;
            OnDead();
        }
    }

    public override void OnDead()
    {
        Debug.Log("Dead");
        Debug.Log("Play Aniamtion and Add Reward To User");
        Destroy(this.gameObject);
    }

}
