using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultProjectlie : ActiveWeapon
{
    protected virtual void Start()
    {

    }

    public override void SetDirection(Vector3 dir)
    {
        base.SetDirection(dir.normalized);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
         rb.velocity = Dir * speed;
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.UserData.SetDmg(GetDmg());
            Destroy(this.gameObject);
        }
        //Destroy(this.gameObject);
    }
}
