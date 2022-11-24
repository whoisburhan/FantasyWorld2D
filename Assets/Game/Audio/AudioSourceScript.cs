using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.AudioAsset
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceScript : MonoBehaviour
    {
        private AudioSource source;
        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Play(AudioAsset asset)
        {
            source.PlayOneShot(asset.Clip);
        }
    }
}