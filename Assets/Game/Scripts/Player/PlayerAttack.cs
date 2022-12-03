using System.Linq;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Projectile;
using System;

namespace GS.FanstayWorld2D.Player
{
    public enum PlayerWeapon
    {
        EmptyHand = 0, Sword, Bow, Wand
    }
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask damagableLayer;
        [Header("Attack Range")]
        [SerializeField] private Transform punchAttackHitBoxPos;
        [SerializeField] private float punchAttackRadius;
        [Space]
        [SerializeField] private Transform swordAttackHitBoxPos;
        [SerializeField] private float swordAttackRadius;
        [Header("Projectile Spawn Point")]
        [SerializeField] private Transform arrowSpawnPoint;
        [SerializeField] private Transform projectileSpawnPoint;

        private int punchAttackPow = 5, swordAttackPow = 10;

        private void OnEnable()
        {
            GameData.OnLoadData += OnStoreDataUpdate;
            GameData.OnSaveData += OnStoreDataUpdate;
        }

        private void OnDisable()
        {
            GameData.OnLoadData -= OnStoreDataUpdate;
            GameData.OnSaveData -= OnStoreDataUpdate;
        }

        private void OnStoreDataUpdate(SelectedStoreData data)
        {
            swordAttackPow = data.Sword.gameBenifits.Attack;
        }

        void Update()
        {
            WeaponSwitch();


            if (PlayerConstant.Instance.CanAttack)
            {
                if (PlayerController.Instance.Attack_1)
                {
                    Attack_1();
                }
            }
        }

        private void WeaponSwitch()
        {
            if (PlayerController.Instance.SwitchWeapon)
            {
                PlayerConstant.Instance.PlayerWeapon++;
            }
        }
        private void Attack_1()
        {
            PlayerAnimation.Instance.Attack();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            MainHandAttack();
        }

        private void MainHandAttack()
        {
            switch ((PlayerWeapon)PlayerConstant.Instance.PlayerWeapon)
            {
                case PlayerWeapon.EmptyHand:
                    PunchAttack();
                    break;
                case PlayerWeapon.Sword:
                    SwordAttack();
                    break;
                case PlayerWeapon.Bow:
                    BowAttack();
                    break;
                case PlayerWeapon.Wand:
                    WandAttack();
                    break;
            }
        }

        private void PunchAttack()
        {
            StartCoroutine(Delay(() =>
            {
                Collider2D[] detectedObj = Physics2D.OverlapCircleAll(punchAttackHitBoxPos.position, punchAttackRadius, damagableLayer);
                if (detectedObj != null)
                {
                    foreach (var col in detectedObj)
                    {
                        Debug.Log(col.name);
                        col.transform.SendMessage("Damage", punchAttackPow);
                    }
                }
            }));
        }
        private void SwordAttack()
        {
            StartCoroutine(Delay(() =>
            {
                Collider2D[] detectedObj = Physics2D.OverlapCircleAll(swordAttackHitBoxPos.position, swordAttackRadius, damagableLayer);
                if (detectedObj != null)
                {
                    foreach (var col in detectedObj)
                    {
                        Debug.Log(col.name);
                        col.transform.SendMessage("Damage", swordAttackPow);
                    }
                }
            }));
        }

        private void BowAttack()
        {
            GameObject go = ProjectileController.Instance.GetActiveProjectile();
            go.SetActive(false);
            go.transform.SetPositionAndRotation(arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            go.SetActive(true);
        }

        private void WandAttack()
        {
            GameObject go = ProjectileController.Instance.GetActiveProjectile();
            go.SetActive(false);
            go.transform.SetPositionAndRotation(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            go.SetActive(true);
        }

        #region  Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(swordAttackHitBoxPos.position, swordAttackRadius);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(punchAttackHitBoxPos.position, punchAttackRadius);

        }

        #endregion

        private IEnumerator Delay(System.Action _action, float delayTime = 0.2f)
        {
            yield return new WaitForSeconds(delayTime);
            _action?.Invoke();
        }
    }
}