using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.AudioAsset
{
    public enum AudioPlayTimeInAnimation
    {
        OnStateEnter, OnStateExit
    }
    public class AudioAnimationSyncScript : StateMachineBehaviour
    {
        private AudioSourceScript audioSourceScript;
        [SerializeField] private List<AudioName> audioName;
        [SerializeField] private AudioPlayTimeInAnimation audioPlayTimeInAnimation;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (audioSourceScript == null)
            {
                audioSourceScript = animator.transform.GetComponent<AudioSourceScript>();

                if (audioSourceScript == null) audioSourceScript = animator.transform.parent.GetComponent<AudioSourceScript>();

                if (audioSourceScript == null) audioSourceScript = animator.transform.parent.parent.GetComponent<AudioSourceScript>();

            }

            if (audioPlayTimeInAnimation == AudioPlayTimeInAnimation.OnStateEnter && audioName.Count > 0)
            {
                int chosenAudio = Random.Range(0, audioName.Count);
                audioSourceScript.Play(AudioManager.Instance.GetAudioClip(audioName[chosenAudio]));
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (audioPlayTimeInAnimation == AudioPlayTimeInAnimation.OnStateExit && audioName.Count > 0)
            {
                int chosenAudio = Random.Range(0, audioName.Count);
                audioSourceScript.Play(AudioManager.Instance.GetAudioClip(audioName[chosenAudio]));
            }
        }

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
}