using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public enum PlayerWeapon
    {
        EmptyHand = 0, Sword, Bow, Wand
    }
    public class PlayerAttack : MonoBehaviour
    {
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
        }
    }
}