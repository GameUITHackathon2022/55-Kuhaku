using System;
using System.Collections;
using System.Collections.Generic;
using UI_InputSystem.Base;
using UnityEngine;
using UnityEngine.Serialization;

public class Chicken : MonoBehaviour
{
    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Distance = 10;
    public Transform chickenTail;
    public Rigidbody rb;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    [SerializeField]
    private List<Vector3> PositionsHistory = new List<Vector3>();

    private bool isMove = false;
    

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (BodyParts.Count <= 0 || PositionsHistory.Count <= 0)
            return;
        
        // Move body parts
        for (var i = 0; i < BodyParts.Count; i++)
        {
            var body = BodyParts[i];
            Vector3 point = PositionsHistory[Mathf.Clamp(i * Distance, 0, PositionsHistory.Count - 1)];
            if (Vector3.Distance(point, body.transform.position) < 1 && !isMove)
                continue;
            else if(Vector3.Distance(point, body.transform.position) < 0.1f)
                continue;
            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection.normalized * BodySpeed * Time.deltaTime;
            // Rotate body towards the point along the snakes path
            body.transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        
        if(PositionsHistory.Count > (BodyParts.Count-1) * Distance)
            PositionsHistory.RemoveRange((BodyParts.Count-1) * Distance,PositionsHistory.Count - (BodyParts.Count-1) * Distance);
    }


    private void FixedUpdate()
    {
        // Move forward
        if (PlayerMovementDirection().magnitude > 0)
        {
            Vector3 nextPos = transform.position + PlayerMovementDirection().normalized * MoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPos);
            // if (PositionsHistory.Count > 0)
            // {
            //     PositionsHistory.Insert(0, chickenTail.position);
            // }
            // else
            // {
            //     PositionsHistory.Insert(0, chickenTail.position);
            // }
            PositionsHistory.Insert(0, chickenTail.position);
            isMove = true;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(PlayerMovementDirection().normalized),0.1f);

        }
        else
        {
            isMove = false;
        }

        // Store position history
    }
    
    Vector3 PlayerMovementDirection()
    {
        Vector3 baseDirection = Vector3.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) +
                                Vector3.forward * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);
        baseDirection *= Time.deltaTime;
        return baseDirection;
    }
    
    public void AddChild(GameObject gObj) {
        // Instantiate body instance and
        // add it to the list
        BodyParts.Add(gObj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChicken>(out ICollisionWithChicken chicken))
        {
            chicken.OnCollisionWithChicken(this);
        }
    }
}
