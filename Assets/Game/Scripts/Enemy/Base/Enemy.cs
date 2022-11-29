using System.Collections;
using System.Collections.Generic;
using GS.AudioAsset;
using GS.FanstayWorld2D.Player;
using UnityEngine;

namespace GS.FanstayWorld2D.Enemy
{
    public enum EnemyState
    {
        None, Idle, Patrol, Chase, Attack, Hurt, Die
    }
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer),typeof(AudioSourceScript))]
    public class Enemy : MonoBehaviour
    {
        protected EnemyState enemyState, previousEnemyState;
        protected Transform chaseObjectTransform;
        protected Animator animator;
        protected int health;

        protected SpriteRenderer sr;
        private bool canCheckState = true;
        private int currentPatrolPoint = 0;

        [SerializeField] protected HealthBarBehaviour enemyHealth;
        [SerializeField] protected int maxHealth = 50;
        [Space]
        [SerializeField] protected int enemyDamagePower = 10;
        [SerializeField] protected bool canChase;
        [SerializeField] protected bool canPetrol;
        [Space]
        [SerializeField] private LayerMask attackMask;
        [SerializeField] private Vector2 chaseZone;
        [SerializeField] private Vector2 chaseZoneOffeset;
        [SerializeField] private float chaseZoneAngle;
        [Space]
        [SerializeField] private Vector2 attakZone;
        [SerializeField] private Vector2 attakZoneOffset;
        [SerializeField] private float attakZoneAngle;
        [Space]
        [SerializeField] protected float chaseSpeed = 2f;
        [Space]
        [SerializeField] private Vector3[] patrolPoints;
        [SerializeField] private bool isPetrolRandomPoint;


        protected void Start()
        {
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            health = maxHealth;
            enemyHealth.SetHealth(health, maxHealth);

            #region Update PetrolPoints

            for (int i = 0; i < patrolPoints.Length; i++)
            {
                var temp = patrolPoints[i];
                patrolPoints[i] = transform.position + temp;
            }

            #endregion

        }
        protected void Update()
        {
            CheckState();
            //if (enemyState != EnemyState.Die) CheckState();

            switch (enemyState)
            {
                case EnemyState.Idle:
                    IdleState();
                    break;
                case EnemyState.Patrol:
                    PatrolState();
                    break;
                case EnemyState.Chase:
                    ChaseState();
                    break;
                case EnemyState.Attack:
                    AttackState();
                    break;
                case EnemyState.Hurt:
                    HurtState();
                    break;
                case EnemyState.Die:
                    DieState();
                    break;
            }
        }

        protected void DiableCheckState()
        {
            canCheckState = false;
            Debug.Log($"000 {enemyState} | canCheckState =  {canCheckState}");
        }

        protected void EnableCheckState()
        {
            canCheckState = true;
            Debug.Log($"000 {enemyState} | canCheckState =  {canCheckState}");
        }
        protected virtual void CheckState()
        {
            Collider2D[] attackZoneDetectedObj = Physics2D.OverlapBoxAll((Vector2)transform.position + attakZoneOffset, attakZone, attakZoneAngle, attackMask);

            if (attackZoneDetectedObj.Length > 0)
            {
                if (canCheckState)
                {
                    previousEnemyState = enemyState;
                    enemyState = EnemyState.Attack;
                    chaseObjectTransform = attackZoneDetectedObj[0].transform;
                }
            }
            else
            {
                if (canChase)
                {

                    Collider2D[] chaseZoneDetectedObj = Physics2D.OverlapBoxAll((Vector2)transform.position + chaseZoneOffeset, chaseZone, chaseZoneAngle, attackMask);

                    if (chaseZoneDetectedObj.Length > 0)
                    {
                        chaseObjectTransform = chaseZoneDetectedObj[0].transform;
                        previousEnemyState = enemyState;

                        if (canCheckState)
                            enemyState = EnemyState.Chase;
                    }

                    else
                    {
                        if (canCheckState)
                        {
                            previousEnemyState = enemyState;
                            enemyState = canPetrol ? EnemyState.Patrol : EnemyState.Idle;
                        }
                    }
                }
                else
                {
                    if (canCheckState)
                    {
                        previousEnemyState = enemyState;
                        enemyState = canPetrol ? EnemyState.Patrol : EnemyState.Idle;
                    }
                }


            }


        }

        ///Makes enemy Direction Towards to Target
        private void CheckAndSetEnemyDirection(Vector3 target)
        {
            bool isDirectionLeft = target.x - transform.position.x < 0f;

            // if (isDirectionLeft != sr.flipX)
            // {
            //     sr.flipX = isDirectionLeft;
            // }

            if((transform.localScale.x > 0f && isDirectionLeft) ||(transform.localScale.x < 0f && !isDirectionLeft))
            {
                transform.localScale =  new Vector3(transform.localScale.x * -1f, transform.localScale.y);
            }
        }
        protected virtual void IdleState()
        {

        }

        protected virtual void PatrolState()
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPoint], chaseSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint]) <= 0.3f)
            {
                currentPatrolPoint = changePatrolPoint();
            }

            CheckAndSetEnemyDirection(patrolPoints[currentPatrolPoint]);
        }

        private int changePatrolPoint()
        {
            if (!isPetrolRandomPoint)
            {
                return (currentPatrolPoint + 1) % patrolPoints.Length;
            }
            else
            {
                int _temp = UnityEngine.Random.Range(0, patrolPoints.Length);

                while (_temp == currentPatrolPoint)
                {
                    _temp = UnityEngine.Random.Range(0, patrolPoints.Length);
                }

                return _temp;
            }
        }

        protected virtual void ChaseState()
        {
            var playerPos = new Vector2(chaseObjectTransform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, playerPos, chaseSpeed * Time.deltaTime);
            CheckAndSetEnemyDirection(chaseObjectTransform.position);
        }

        protected virtual void AttackState()
        {
            CheckAndSetEnemyDirection(chaseObjectTransform.position);
        }

        protected virtual void GiveDamage()
        {
            Collider2D[] attackZoneDetectedObj = Physics2D.OverlapBoxAll((Vector2)transform.position + attakZoneOffset, attakZone, attakZoneAngle, attackMask);

            if (attackZoneDetectedObj.Length > 0)
            {
                foreach (var obj in attackZoneDetectedObj)
                {
                    obj.SendMessage("Damage", enemyDamagePower);
                }
            }
        }

        protected virtual void HurtState()
        {

        }

        protected virtual void DieState()
        {

        }

        #region  Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + attakZoneOffset, attakZone);

            if (canChase)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube((Vector2)transform.position + chaseZoneOffeset, chaseZone);
            }

            if (canPetrol)
            {
                Gizmos.color = new Color(1f, 0.3f, 0f); // Orange Color

                if (Application.isPlaying)
                {
                    foreach (var patrolPoint in patrolPoints)
                    {
                        Gizmos.DrawSphere(patrolPoint, 0.3f);
                    }
                }
                else
                {
                    foreach (var petrolPoint in patrolPoints)
                    {
                        Gizmos.DrawSphere(transform.position + petrolPoint, 0.3f);
                    }
                }
            }
        }

        #endregion

        #region Enemy Health

        public void Damage(int damageAmount)
        {
            health -= damageAmount;
            enemyState = EnemyState.Hurt;
            enemyHealth.SetHealth(health, maxHealth);

            if (health <= 0)
            {
                Debug.Log("ENEMEY DIED");
                canCheckState = false;
                enemyState = EnemyState.Die;
                transform.GetComponent<Collider2D>().enabled = false;
                DeathAnimation();
            }
            else
            {
                HurtAnimation();
            }
        }

        protected virtual void HurtAnimation() { }

        protected virtual void DeathAnimation() { }
        protected void DeactivateEnemy()
        {
            gameObject.SetActive(false);
        }
        #endregion


    }
}