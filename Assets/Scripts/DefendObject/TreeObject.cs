using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : DefendBase
{
    /// <summary>
    /// Used to check visible
    /// </summary>
    Renderer renderer;
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
        //renderer = GetComponent<Renderer>();
    }

//    private void Update()
//    {
//#if UNITY_EDITOR
//        if (ischeck)
//        {
//            if (renderer != null && renderer.isVisible)
//            {
//                Debug.Log("Object is visual");
//            }
//        }
//#endif
//    }

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
