using System;
using System.Collections;
using System.Collections.Generic;
using UI_InputSystem.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Chicken : MonoSingleton<Chicken>
{
    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Distance = 10;
    public int chickenHealth;
    private int currentHealth;
    
    [Header("Reference")]
    [SerializeField] Transform chickenTail;
    [SerializeField] Transform chickenVisual;
    [SerializeField] Rigidbody rb;
    [SerializeField] private ChickenAnimationController _chickenAnimationController;
    [SerializeField] private Image healFill;
    
    // Lists
    private List<Chip> BodyParts = new List<Chip>();
    [SerializeField]
    private List<Vector3> PositionsHistory = new List<Vector3>();
    private bool isMove = false;

    private void Awake()
    {
        currentHealth = chickenHealth;
    }
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (BodyParts.Count <= 0 || PositionsHistory.Count <= 0)
            return;
        
        // Move body parts
        for (var i = 0; i < BodyParts.Count; i++)
        {
            var chipElement = BodyParts[i];
            Vector3 point = PositionsHistory[Mathf.Clamp(i * Distance, 0, PositionsHistory.Count - 1)];
            if (Vector3.Distance(point, chipElement.transform.position) < 1 && !isMove)
            {
                chipElement.SetSpeedAnim(0);
                continue;
            }
            
            if(Vector3.Distance(point, chipElement.transform.position) < 0.1f)
                continue;
            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - chipElement.transform.position;
            chipElement.transform.position += moveDirection.normalized * BodySpeed * Time.deltaTime;
            // Rotate body towards the point along the snakes path
            chipElement.transform.rotation = Quaternion.LookRotation(moveDirection);
            chipElement.SetSpeedAnim(1);
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
            PositionsHistory.Insert(0, chickenTail.position);
            isMove = true;
            // transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(PlayerMovementDirection().normalized),0.1f);
            chickenVisual.rotation = Quaternion.Slerp(chickenVisual.rotation,Quaternion.LookRotation(PlayerMovementDirection().normalized),0.1f);
            _chickenAnimationController.SetSpeed(1);
        }
        else
        {
            isMove = false;
            _chickenAnimationController.SetSpeed(0);
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
    
    public void AddChild(Chip gObj) {
        // Instantiate body instance and
        // add it to the list
        BodyParts.Add(gObj);
    }

    public void OnTakeDamage(int damage)
    {
        currentHealth -= damage;
        float percent = ((float)currentHealth / chickenHealth);
        healFill.fillAmount = 1 - percent;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChicken>(out ICollisionWithChicken chicken))
        {
            chicken.OnCollisionWithChicken(this);
        }
    }
}
