using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        public static PlayerAnimation Instance { get; private set; }
        [SerializeField] private Animator animator;
        [SerializeField] private Animator memaidAnimator;

        int run, sprint, jump, grounded, canClimb, climbUp, climbDown, weaponType, attack, swime, hit, die;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            run = Animator.StringToHash("_Run");
            sprint = Animator.StringToHash("_Sprint");
            jump = Animator.StringToHash("_Jump");
            grounded = Animator.StringToHash("_IsGrounded");
            canClimb = Animator.StringToHash("_Climb");
            climbUp = Animator.StringToHash("_ClimbUp");
            climbDown = Animator.StringToHash("_ClimbDown");
            weaponType = Animator.StringToHash("_WeaponType");
            attack = Animator.StringToHash("_Attack");
            swime = Animator.StringToHash("_Swime");
            hit = Animator.StringToHash("_Hit");
            die = Animator.StringToHash("_Die");
        }

        public void ForcefullyIdle()
        {
            animator.Play("Idle");
        }

        public void Run()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(run, true);
            else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                memaidAnimator.SetBool(run, true);
        }

        public void Sprint()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(sprint, true);
            else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                memaidAnimator.SetBool(sprint, true);
        }

        public void StopRun()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(run, false);
            else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                memaidAnimator.SetBool(run, false);
        }

        public void StopSprint()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(sprint, false);
            else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                memaidAnimator.SetBool(sprint, false);
        }

        public void NotGrounded()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(grounded, false);
        }

        public void Grounded()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(grounded, true);
        }

        public void Jump()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetTrigger(jump);
        }

        #region Climb
        public void CanClimb(bool can)
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(canClimb, can);
        }

        public void CanClimbUp(bool can)
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(climbUp, can);
        }

        public void CanClimbDown(bool can)
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(climbDown, can);
        }

        #endregion

        #region Hit and Die

        public void Hit()
        {
            animator.SetTrigger(hit);
        }

        public void Die(bool status = true)
        {
            animator.SetBool(die, status);
        }

        #endregion

        #region  Attack

        public void SwitchWeapon(int _weaponType)
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetInteger(weaponType, _weaponType);
        }

        public void Attack()
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetTrigger(attack);
            else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                memaidAnimator.SetTrigger(attack);
        }

        #endregion

        #region  Swime

        public void CanSwime(bool can)
        {
            if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                animator.SetBool(swime, can);
        }

        #endregion
    }
}