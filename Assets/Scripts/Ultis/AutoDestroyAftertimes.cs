using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAftertimes : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject,0.2f);
    }
}
