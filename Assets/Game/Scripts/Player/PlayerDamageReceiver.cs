using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerDamageReceiver : MonoBehaviour
    {
        private PlayerHealth playerHealth;

        private void Awake()
        {
            playerHealth = transform.parent.GetComponent<PlayerHealth>();
        }

        private void Damage(int damageAmount)
        {
            playerHealth.Damage(damageAmount);
        }
    }
}