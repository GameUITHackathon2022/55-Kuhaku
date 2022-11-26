using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultProjectlie : ActiveWeapon
{
    [SerializeField] protected Rigidbody rb;
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

    protected override void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        if(collider.gameObject.CompareTag("Player"))
        {
            //PlayerManager.Instance.UserData.SetDmg(GetDmg());
            var baseDefend = collider.gameObject.GetComponent<DefendBase>();
            if(baseDefend != null)
            {
                baseDefend.TakeDamage(1);
            }

            Destroy(this.gameObject);
        }
        //Destroy(this.gameObject);
    }
}
