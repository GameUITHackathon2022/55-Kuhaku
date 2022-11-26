using Enemy;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    protected Transform player => PlayerManager.Instance.transform;
    public Transform Target => player;
    public virtual EnemyType EnemyType => EnemyType.Melee;
    #region Enemy AI
    [Header("AI Component")]

    [SerializeField] protected Collider colliderEnemy;
    [SerializeField] Transform dirLooking;
    [SerializeField] protected Rigidbody thisRG;

    protected bool InDistance()
    {
        var dis = Vector3.Distance(player.position, transform.position);
        return dis <= enemyStatus.rangeAttack;
    }
    
    protected bool CanAttack => InDistance() && timeCd <= 0;

    protected virtual void OnChasePlayer()
    {
        if(!InDistance())
        {
            Vector3 dir = (player.position - transform.position).normalized * enemyStatus.enemySpeed;
            Vector3 directionHeading = new Vector3(dir.x, 0, dir.z);
            thisRG.velocity = directionHeading;
        }
        else
        {
            //thisRG.velocity = Vector3.zero;
        }
    }

    private float timeCd;
    public virtual void OnDoAttack()
    {
        if(timeCd <= 0)
        {
            thisRG.Sleep();
            weaponBase.DmgUser();
            ResetStat();
        }
    
    }
    public virtual void OnPlayCoolDown()
    {
        timeCd -= Time.deltaTime;
    }


    #endregion

    #region Enemy Status Handler
    [Header("Enemy Status")]
    [SerializeField] protected EnemyStatus enemyStatus;
    public virtual void SetStatsByConfig()
    {

    }
    
    public virtual void ResetStat()
    {
        timeCd = enemyStatus.cdEnemey;
    }

    public virtual void SetStatus()
    {
        SetWeapon();
    }

    public virtual void DestroyThisEnemy()
    {
        SpawnEnemyManager.Instance.UnAssignEnemy(this);
        Destroy(this.gameObject);
    }

    public void EnemyTakeDmg(float dmg, UnityAction unityAction)
    {
        enemyStatus.enemyHp -= dmg;
        unityAction?.Invoke();
        if(enemyStatus.enemyHp <= 0)
        {
            DestroyThisEnemy();
        }
    }

    protected void DoLookTarget(Transform target)
    {
        Vector3 notCount = new Vector3(target.position.x, transform.position.y, target.position.z);
        //dirLooking.LookAt(target.position, -Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation((notCount - transform.position).normalized), 0.1f);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

    }

    public virtual float GetDmg => enemyStatus.cdEnemey;
    public virtual float GetCoolDown => enemyStatus.cdEnemey;
    public virtual float GetRange => enemyStatus.rangeAttack;
    #endregion

    #region Ref Weapon
    [SerializeField] protected WeaponBase weaponBase;
    public WeaponBase WeaponBase => weaponBase;
    public void SetWeaponFromConfig(WeaponBase weaponBase)
    {
        this.weaponBase = weaponBase;
    }
    public void SetWeapon()
    {
        if(weaponBase == null)
        {
            Debug.LogError($"You don't assign");
            return;
        }

        weaponBase.SetHolder(this);
    }
    #endregion

    #region MonoHandler

    public virtual void Start()
    {
        
        SetStatus();
    }

    protected virtual void FixedUpdate()
    {
        DoLookTarget(player);
        OnPlayCoolDown();
        //if (transform.name == "Melee") { }
        if (CanAttack)
        {
            //thisRG.velocity = Vector3.zero;
            //Debug.Log($"Hi {Time.deltaTime}");
            OnDoAttack();
        }
        else
        { 
            OnChasePlayer();
        }
    }
    public virtual void OnDestroy()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, enemyStatus.rangeAttack);
    }

    #endregion


}

namespace Enemy
{
    public static class EnemyDefine
    {
        public static readonly float RangeRangedEnemy = 6f;
        public static readonly float RangeMeleeEnemy = 3.3f;
        public static readonly string playerTag = "Player";
        public static readonly string terrainTag = "Terrain";
        public static readonly string enemyTag = "Enemy";
    }

    public enum EnemyType
    {
        Melee, 
        Ranged,
    }

    public enum MeleeEnemyType
    {
        Default,
        SpawnDouble,
        RushAttack,
    }

    [System.Serializable]
    public class EnemyStatus
    {
        public float enemyHp;
        public float enemySpeed;

        public float cdEnemey;
        public float rangeAttack;
    }
}
