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

    [Header("Reference")] public ChickDefend defend;
    [SerializeField] Transform chickenTail;
    [SerializeField] Transform chickenVisual;
    [SerializeField] Rigidbody rb;
    [SerializeField] private ChickenAnimationController _chickenAnimationController;
    //[SerializeField] private Image healFill;

    // Lists
    public List<Chip> BodyParts = new List<Chip>();
    [SerializeField] private List<Vector3> PositionsHistory = new List<Vector3>();
    private bool isMove = false;

    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(isMove)
            PositionsHistory.Insert(0, chickenTail.position);

        if (BodyParts.Count <= 0 || PositionsHistory.Count <= 0)
            return;

        // Move body parts
        for (var i = 0; i < BodyParts.Count; i++)
        {
            var chipElement = BodyParts[i];
            Vector3 point = PositionsHistory[Mathf.Clamp(i * Distance, 0, PositionsHistory.Count - 1)];
            if (BodyParts.Count == 1)
            {
                point = chickenTail.transform.position;
                point.y = chipElement.transform.position.y;
            }

            if (Vector3.Distance(point, chipElement.transform.position) < 0.1 && !isMove)
            {
                chipElement.SetSpeedAnim(0);
                continue;
            }

            // if(Vector3.Distance(point, chipElement.transform.position) < 0.1f)
            //     continue;
            // Move body towards the point along the snakes path

            Vector3 moveDirection = point - chipElement.transform.position;
            chipElement.transform.position += moveDirection.normalized * BodySpeed * Time.deltaTime;
            // Rotate body towards the point along the snakes path
            // if(Vector3.Dot(moveDirection,chipElement.transform.forward) > 0)
                chipElement.transform.rotation = Quaternion.Slerp(chipElement.transform.rotation,Quaternion.LookRotation(moveDirection.normalized * BodySpeed),0.05f);
            chipElement.SetSpeedAnim(1);
        }

        if (PositionsHistory.Count > (BodyParts.Count) * Distance)
            PositionsHistory.RemoveRange((BodyParts.Count - 1) * Distance,
                PositionsHistory.Count - (BodyParts.Count - 1) * Distance);
    }


    private void FixedUpdate()
    {
        // Move forward
        if (PlayerMovementDirection().magnitude > 0)
        {
            Vector3 nextPos = transform.position +
                              PlayerMovementDirection().normalized * MoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPos);
            isMove = true;
            // transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(PlayerMovementDirection().normalized),0.1f);
            chickenVisual.rotation = Quaternion.Slerp(chickenVisual.rotation,
                Quaternion.LookRotation(PlayerMovementDirection().normalized), 0.1f);
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

    public void AddChild(Chip gObj)
    {
        // Instantiate body instance and
        // add it to the list
        BodyParts.Add(gObj);
    }

    public void OnTakeDamage(int damage)
    {
        defend.currentHp -= damage;
        float percent = ((float)defend.currentHp / defend.maxHp);
        //healFill.fillAmount = percent;
        FollowHealthBar.Instance.SetFill(percent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollisionWithChicken>(out ICollisionWithChicken chicken))
        {
            chicken.OnCollisionWithChicken(this);
        }
    }
}