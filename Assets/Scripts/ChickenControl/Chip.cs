using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChip>(out ICollisionWithChip col))
        {
            col.OnCollisionWithChip(this.gameObject);
        }
            
    }
}
