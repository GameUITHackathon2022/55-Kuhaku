using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private void Start()
    {
        //this.transform.DOShakePosition(0.8f, new Vector3(0.1f, 0f, 0f)).SetLoops(-1);
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

    public override void TakeDamage(float damage)
    {
        currentHp = maxHp - currentHp;
        this.transform.DOShakePosition(0.8f, new Vector3(0.1f, 0f, 0f));
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
