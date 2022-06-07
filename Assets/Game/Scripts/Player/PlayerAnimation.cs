using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance { get; private set; }
    [SerializeField] private Animator animator;

    int run, sprint, jump, grounded, canClimb, climbUp,climbDown, weaponType, attack, swime;
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
    }

    public void Run()
    {
        animator.SetBool(run, true);
    }

    public void Sprint()
    {
        animator.SetBool(sprint, true);
    }

    public void StopRun()
    {
        animator.SetBool(run, false);
    }

    public void StopSprint()
    {
        animator.SetBool(sprint, false);
    }

    public void NotGrounded()
    {
        animator.SetBool(grounded, false);
    }

    public void Grounded()
    {
        animator.SetBool(grounded, true);
    }

    public void Jump()
    {
        animator.SetTrigger(jump);
    }

    #region Climb
    public void CanClimb(bool can)
    {
        animator.SetBool(canClimb,can);
    }

    public void CanClimbUp(bool can)
    {
        animator.SetBool(climbUp,can);
    }

    public void CanClimbDown(bool can)
    {
        animator.SetBool(climbDown,can);
    }

    #endregion

    #region  Attack

    public void SwitchWeapon(int _weaponType)
    {
        animator.SetInteger(weaponType, _weaponType);
    }

    public void Attack()
    {
        animator.SetTrigger(attack);
    }

    #endregion

    #region  Swime

    public void CanSwime(bool can)
    {
        animator.SetBool(swime,can);
    }
    
    #endregion
}
