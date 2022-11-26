using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    [SerializeField] Image hpFill;
    public void SetFillBar(float fill)
    {
        hpFill.fillAmount = fill;
    }
}
