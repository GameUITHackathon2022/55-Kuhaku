using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public float hp;
    public void SetDmg(float dmg)
    {
        hp -= dmg;
    }
}
