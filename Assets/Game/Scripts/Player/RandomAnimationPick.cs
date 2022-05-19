using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationPick : StateMachineBehaviour
{
    [SerializeField] private int animationCount;
    [SerializeField] private bool longDelayToVariation = false;
    [SerializeField] private int turnToWaitForRandomnese = 3;

    private int counter = 0 , animationNo = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!longDelayToVariation)
        {
            animator.SetInteger("RandomPick", Random.Range(0, animationCount));
        }
        else
        {
            counter++;
            if(counter >= turnToWaitForRandomnese)
            {
                animationNo = Random.Range(0, animationCount);
                counter = 0;
            }
            
            animator.SetInteger("RandomPick", animationNo);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
