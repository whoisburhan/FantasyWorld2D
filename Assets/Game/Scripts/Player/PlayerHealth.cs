using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private int currentHealth = 50;
        private int maxHealth;

        public void Damage(int damageAmount)
        {
            if (currentHealth > 0)
            {
                PlayerConstant.Instance.BloodParticles.Play();

                currentHealth = Mathf.Max(0, currentHealth - damageAmount);

                if (currentHealth > 0)
                {
                    PlayerAnimation.Instance.Hit();
                    
                }
                else
                {
                    Dead();
                }
                
            }
        }
        private void Dead()
        {
            PlayerAnimation.Instance.Die();
        }
    }
}