using Enemy;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    private static readonly float rangeToStop = 5f;
    [SerializeField] protected DefendBase defaultTarget;
    [SerializeField] protected DefendBase _player;
    [SerializeField] protected DefendBase _crrTarget;
    public Transform Target => _crrTarget.transform;
    public virtual EnemyType EnemyType => EnemyType.Melee;

    private void Awake()
    {
        crrHp = enemyStatus.enemyHp;
        EnemyTakeDmg(0, null);
        isReadyAttack = false;
        _crrTarget = defaultTarget;
    }

    #region Enemy AI
    [Header("AI Component")]

    [SerializeField] protected Collider colliderEnemy;
    [SerializeField] protected Rigidbody thisRG;
    [SerializeField] protected bool isReadyAttack;
    protected virtual bool InDistance()
    {
        var dis = Vector3.Distance(Target.position, transform.position);
        return dis <= enemyStatus.rangeAttack;
    }
    
    protected bool CanAttack => InDistance() && timeCd <= 0;

    protected virtual void OnChasePlayer()
    {
        if(!InDistance())
        {
            Vector3 dir = (Target.position - transform.position).normalized * enemyStatus.enemySpeed;
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
        if(InDistance())
        {
            thisRG.Sleep();
            weaponBase.DmgUser();
            //ResetStat();
        }
    
    }
    public virtual void OnPlayCoolDown()
    {
        timeCd -= Time.deltaTime;
    }

    public void SetTarget(DefendBase defendBase)
    {
        this._crrTarget = defendBase;
    }
    #endregion

    #region Enemy Status Handler
    private float crrHp;
    [Header("Enemy Status")]
    [SerializeField] protected EnemyStatus enemyStatus;
    [SerializeField] protected EnemyUI enemyUI;
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
        crrHp -= dmg;
        float percent = crrHp / enemyStatus.enemyHp;
        enemyUI.SetFillBar(percent);

        unityAction?.Invoke();
        if(crrHp <= 0)
        {
            DestroyThisEnemy();
        }
    }

    protected void DoLookTarget(Transform target)
    {
        Vector3 notCount = new Vector3(target.position.x, transform.position.y, target.position.z);

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
        if (_crrTarget == null)
            return;

        DoLookTarget(Target);
        OnPlayCoolDown();
        //if (transform.name == "Melee") { }

        if (_player != null && _crrTarget == _player)
        { 
            if(Vector3.Distance(_player.transform.position, transform.position) >= rangeToStop)
            {
                SetTarget(defaultTarget);
            }
        }

        if (CanAttack)
        {
            //OnDoAttack();
            animator.TriggerAttack();
            ResetStat();
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

    #region Animator Handler
    [SerializeField] protected EnemyAnimatorController animator;
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
