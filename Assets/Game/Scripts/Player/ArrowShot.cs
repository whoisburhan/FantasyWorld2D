using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ArrowShot : MonoBehaviour
    {
        public Vector2 Direction;

        [SerializeField] private LayerMask damageble;
        [SerializeField] private float speed = 2f;
        [SerializeField] private int damageAmount = 5;
        [SerializeField] private float radius = 0.57f;

        private Rigidbody2D rb2d;

        private void Awake()
        {
            if(rb2d == null)    rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            GiveDamage();
            rb2d.velocity = Direction * speed;
        }

        private void GiveDamage()
        {
            Collider2D[] attackZoneDetectedObj = Physics2D.OverlapCircleAll((Vector2)transform.position, radius, damageble);

            if (attackZoneDetectedObj.Length > 0)
            {
                foreach (var obj in attackZoneDetectedObj)
                {
                    // obj.SendMessage("Damage", damageAmount);
                }

                Destroy(this.gameObject);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}