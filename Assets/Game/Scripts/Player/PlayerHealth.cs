using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.UI;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private int currentHealth = 100;
        private int maxHealth = 100;
        private float lowHealthPercentage = 0.2f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H)) 
            {
                currentHealth = Mathf.Min(currentHealth + 20, maxHealth);
                OnGamePlayUI.Instance.UpdateHealthUI(currentHealth, maxHealth, lowHealthPercentage);
                PlayerAnimation.Instance.Die(false);
            }
        }
        public void Damage(int damageAmount)
        {
            if (currentHealth > 0)
            {
                PlayerConstant.Instance.BloodParticles.Play();

                currentHealth = Mathf.Max(0, currentHealth - damageAmount);

                OnGamePlayUI.Instance.UpdateHealthUI(currentHealth, maxHealth, lowHealthPercentage);

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