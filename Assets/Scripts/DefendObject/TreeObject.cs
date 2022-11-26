using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TreeObject : DefendBase
{
    /// <summary>
    /// Used to check visible
    /// </summary>
    Renderer renderer;

    public Image healthBar;
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

    public bool seen;

    private void Awake()
    {
        LEVEL = level;
    
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        //this.transform.DOShakePosition(0.8f, new Vector3(0.1f, 0f, 0f)).SetLoops(-1);
    }


    private void Update()
    {

        if (!seen)
        {
            if (renderer != null && renderer.isVisible)
            {
                seen = true;
                //Debug.Log(" seen");
            }
        }

    }

    public override void TakeDamage(int damage)
    {
        if (!seen)
        {
            return;
        }
        currentHp -= damage;
        this.transform.DOShakePosition(0.8f, new Vector3(0.1f, 0f, 0f));
        SetFill(currentHp / maxHp);
        if (currentHp < 0)
        { 
            currentHp = 0;
            OnDead();
        }
    }

    public void SetFill(float percent)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = percent;
        }  
    }

    public override void OnDead()
    {
        UIScoreGame.Instance.SetCurrentTree();
        Destroy(this.gameObject);
    }

}
