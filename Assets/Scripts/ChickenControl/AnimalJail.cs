using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalJail : MonoBehaviour, ICollisionWithChicken
{
    [SerializeField] private Chip animal;
    public void OnCollisionWithChicken(Chicken chicken)
    {
        var anim = Instantiate(animal);
        anim.transform.position = this.transform.position;
        chicken.AddChild(anim);
        Destroy(this.gameObject);
    }
}
