using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionWithChicken
{
   void OnCollisionWithChicken(Chicken chicken);
}

public interface ICollisionWithChip
{
   void OnCollisionWithChip(GameObject gameObject);
}
