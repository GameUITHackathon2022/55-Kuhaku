using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField] private Chicken baseChicken;

    #if UNITY_EDITOR

    private void OnValidate()
    {
        baseChicken = FindObjectOfType<Chicken>();

    }

#endif
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChip>(out ICollisionWithChip col))
        {
            col.OnCollisionWithChip(this.gameObject);
        }
            
    }
}
