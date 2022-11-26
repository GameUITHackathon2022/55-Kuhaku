using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRangeEnemy : RangeEnemy
{
    public BombProjectile bombProjectile; // Add Shell game object in the Inspector.
    public Transform shellSpawnPos; // Add Cube game object in the Inspector.
    public GameObject parent; // Add Tank game object in the Inspector.
    float speed = 15;
    float turnSpeed = 2;
    bool canShoot = true;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //Vector3 direction = (Target.transform.position - parent.transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        //float? angle = RotateTurret();
        DoLookTarget(player);
        if (CanAttack) // When the angle is less than 10 degrees...
                OnDoAttack(); // ...start firing.
    }

    void CanShootAgain()
    {
        canShoot = true;
    }

    public override void OnDoAttack()
    {
        base.OnDoAttack();
        if (canShoot)
        {
            BombProjectile shell = Instantiate(bombProjectile, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
            //shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward; // Use 'forward' because it's the Z axis you want to shoot along.
            shell.rigidbody.velocity = speed * this.transform.forward;
            canShoot = false;
            
            Invoke("CanShootAgain", 0.5f); // Increase value to slow down rate of fire, decrease value to speed up rate of fire.
        }
    }



    float? RotateTurret()
    {
        float? angle = CalculateAngle(true); // Set to false for upper range of angles, true for lower range.

        if (angle != null) // If we did actually get an angle...
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f); // ... rotate the turret using EulerAngles because they allow you to set angles around x, y & z.
        }
        return angle;
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = Target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);

        }
        else
            return null;
    }
}
