using Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    //[SerializeField] protected float rangeToStop = 5f;
    private Vector3 defaultRotation;
    [SerializeField] protected DefendBase defaultTarget;
    [SerializeField] protected DefendBase _player;
    [SerializeField] protected DefendBase _crrTarget;
    public Transform Target => _crrTarget.transform;
    public DefendBase Crr => _crrTarget;
    public virtual EnemyType EnemyType => EnemyType.Melee;

    public Image healthBar;
    private float _rangeStalkStart;
    private float _rangeStalkEnd;
    private void Awake()
    {
        //_rangeStalkStart = rangeToStop;
        //_rangeStalkEnd = rangeToStop * 1.25f;
        crrHp = enemyStatus.enemyHp;
        EnemyTakeDmg(0, null);
        _crrTarget = defaultTarget;
        _player = Chicken.Instance.defend;

    }

    #region Enemy AI
    [Header("AI Component")]

    [SerializeField] protected Collider colliderEnemy;
    [SerializeField] protected Rigidbody thisRG;

    public Rigidbody GetRigidbody => thisRG;
    protected virtual bool InDistance()
    {
        Vector3 vecto = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        var dis = Vector3.Distance(vecto, transform.position);
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
        if(defendBase == defaultTarget)
        {
            //rangeToStop = _rangeStalkStart;
            //animator.TriggerIddle();

        }
        else
        {
            //animator.TriggerRun();

            //rangeToStop = _rangeStalkEnd;
        }
        this._crrTarget = defendBase;
    }
    #endregion

    #region Enemy Status Handler
    protected int crrHp;
    [Header("Enemy Status")]
    [SerializeField] Transform headLook;
    [SerializeField] protected EnemyStatus enemyStatus;
    //[SerializeField] protected EnemyUI enemyUI;
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

    public virtual void EnemyTakeDmg(int dmg, UnityAction unityAction)
    {
        crrHp -= dmg;
        float percent = (float)crrHp / enemyStatus.enemyHp;
        //enemyUI.SetFillBar(percent);
        healthBar.fillAmount = percent;
        SetTarget(Chicken.Instance.defend);
        unityAction?.Invoke();

        VFXManager.Instance.PlayHitPlayerMelee(transform.position);

        if(crrHp <= 0)
        {
            DestroyThisEnemy();
        }
    }

    public void SetFill(float percent)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = percent;
        }
    }

    protected void DoLookTarget(Transform target)
    {
        Vector3 notCount = new Vector3(target.position.x, transform.position.y, target.position.z);

        headLook.localRotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation((notCount - transform.position).normalized), 1f);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

    }

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

        OnPlayCoolDown();
        DoLookTarget(Target);
        //if (transform.name == "Melee") { }

        if (_player != null && _crrTarget == _player)
        {
            //var v = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            //var distance = Vector3.Distance(v, transform.position);
            //if (distance > rangeToStop)
            //{
            //    SetTarget(defaultTarget);
            //}
            if (thisRG.velocity.magnitude > 0)
            {
                animator.TriggerRun();
            }
            else
            {
                animator.TriggerIddle();
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
        VFXManager.Instance.SpawnFXExplo(this.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, enemyStatus.rangeAttack);

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(this.transform.position, rangeToStop);
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
        public int enemyHp;
        public int enemySpeed;

        public float cdEnemey;
        public int rangeAttack;
    }
}