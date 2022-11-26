using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{

    [SerializeField] private ChickenAnimationController _chickenAnimationController;


    public void SetSpeedAnim(float speed)
    {
        _chickenAnimationController.SetSpeed(speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChip>(out ICollisionWithChip col))
        {
            col.OnCollisionWithChip(this.gameObject);
        }
            
    }
}
