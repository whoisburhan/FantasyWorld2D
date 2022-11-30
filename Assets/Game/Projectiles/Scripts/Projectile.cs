using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Projectile
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask damagable;
        private int attackPower = 10;
        private float projectTileSpeed = 300f;
        private Rigidbody2D rb2d;
        private Animator animator;
        private static float lifeShell = 10f;
        private float lifeShellCounter;
        private bool projectileHasCollision = false;
        private int startAnimation, endAnimation;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            startAnimation = Animator.StringToHash("Start");
            endAnimation = Animator.StringToHash("End");
            
            Physics2D.IgnoreLayerCollision(9,0);
            
        }

        private void OnEnable()
        {
            animator.Play(startAnimation);
            lifeShellCounter = lifeShell;
            projectileHasCollision = false;
        }

        private void Update()
        {
            lifeShellCounter -= Time.deltaTime;
            if (lifeShellCounter <= 0 && !projectileHasCollision)
            {
                DeActivateProjectile();
            }
        }

        private void FixedUpdate()
        {
            if (!projectileHasCollision)
                rb2d.velocity = transform.right * projectTileSpeed * Time.deltaTime;
        }

        public static void UpdateLifeShell(float newLifeShell)
        {
            lifeShell = newLifeShell;
        }

        public void DeActivateProjectile()
        {
            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {

            
            Debug.Log($"COLLIDE {col.gameObject.name}");
            if (!projectileHasCollision)
            {
                animator.Play(endAnimation);
                projectileHasCollision = true;
                rb2d.velocity = Vector2.zero;
            }
            
            if(col.gameObject.layer == 7 || col.gameObject.layer == 8)
                col.gameObject.SendMessage("Damage", attackPower);
        }

    }
}