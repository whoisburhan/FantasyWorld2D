using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.AudioAsset;

public class RandomAnimationPick : StateMachineBehaviour
{
    [SerializeField] protected int animationCount;
    [SerializeField] protected bool longDelayToVariation = false;
    [SerializeField] protected int turnToWaitForRandomnese = 3;

    private int counter = 0;
    protected int animationNo = 0;

    protected AudioSourceScript audioSourceScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSourceScript == null) audioSourceScript = animator.transform.parent.GetComponent<AudioSourceScript>();

        if (!longDelayToVariation)
        {
            animationNo = Random.Range(0, animationCount);
            animator.SetInteger("RandomPick", animationNo);
        }
        else
        {
            counter++;
            if (counter >= turnToWaitForRandomnese)
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
