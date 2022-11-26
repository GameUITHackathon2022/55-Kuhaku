using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalJail : MonoBehaviour, ICollisionWithChicken
{
    [SerializeField] private Chip animal;
    public void OnCollisionWithChicken(Chicken chicken)
    {
        SoundManager.Instance.Play("ChipAwake",AudioType.FX);
        var anim = Instantiate(animal);
        anim.transform.position = this.transform.position;
        chicken.AddChild(anim);
        VFXManager.Instance.PlaySpawnChicken(new Vector3(chicken.transform.position.x,chicken.transform.position.y+1f,chicken.transform.position.z));
        Destroy(this.gameObject);
    }
}
