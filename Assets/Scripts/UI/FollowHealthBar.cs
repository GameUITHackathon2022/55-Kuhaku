using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowHealthBar : MonoSingleton<FollowHealthBar>
{
    private Transform targetTf;
    public Image fillImage;

    private void Awake()
    {
        targetTf = Chicken.Instance.transform;
    }

    private void Update()
    {
        if (targetTf != null)
        {
            this.transform.position = targetTf.position;
        }
    }

    public void SetFill(float percent)
    {
        fillImage.fillAmount = percent;
    }
}
