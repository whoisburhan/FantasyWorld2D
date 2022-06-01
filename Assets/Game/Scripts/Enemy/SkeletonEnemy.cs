using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D;

namespace GS.FanstayWorld2D.Enemy
{
    public enum SkeletonEnemyType
    {
        Skeleton_1 = 0, Skeleton_2_Mage, Skeleton_3
    }
    public class SkeletonEnemy : Enemy
    {
        private int idle, walk, attack, die, hurt, enemyNo;

        [SerializeField] SkeletonEnemyType skeletonEnemyType;
        [SerializeField] private GameObject fireBallPrefab;
        [SerializeField] private Transform fireBallSpawnPoint;

        private void Start()
        {
            base.Start();

            idle = Animator.StringToHash("_Idle");
            walk = Animator.StringToHash("_Walk");
            attack = Animator.StringToHash("_Attack");
            die = Animator.StringToHash("_Die");
            hurt = Animator.StringToHash("_Hurt");
            enemyNo = Animator.StringToHash("_EnemyNo");

            animator.SetInteger(enemyNo, (int)skeletonEnemyType);
        }

        private void Update()
        {
            base.Update();
        }

        protected override void CheckState()
        {
            base.CheckState();

            if (enemyState != previousEnemyState)
            {
                switch (enemyState)
                {
                    case EnemyState.Idle:
                        animator.SetTrigger(idle);
                        break;
                    case EnemyState.Chase:
                    case EnemyState.Patrol:
                        animator.SetTrigger(walk);
                        break;
                    case EnemyState.Attack:
                        animator.SetTrigger(attack);
                        break;
                    case EnemyState.Hurt:
                        //animator.SetTrigger(hurt);
                        break;
                    case EnemyState.Die:
                        //animator.SetTrigger(die);
                        break;
                }
            }
        }

        protected override void HurtAnimation()
        {
            base.HurtAnimation();
            animator.SetTrigger(hurt);
        }

        protected override void DeathAnimation()
        {
            base.DeathAnimation();
            animator.SetTrigger(die);
        }

        public void Attack()
        {
            GameObject go = Instantiate(fireBallPrefab,fireBallSpawnPoint.position, Quaternion.identity);
            
            Player.ArrowShot attackScript = go.GetComponent<Player.ArrowShot>();

            if(attackScript != null)
            {
                attackScript.Direction = sr.flipX ? Vector2.left : transform.localScale.x > 0 ? Vector2.right :Vector2.left;
            }

            Destroy(go, 2f);
        }

    }
}