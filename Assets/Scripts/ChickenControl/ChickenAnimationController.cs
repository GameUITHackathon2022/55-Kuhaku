using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeed(float speed)
    {
        _animator.SetFloat("speed",speed);
    }
}
