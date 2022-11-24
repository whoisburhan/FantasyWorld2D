using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.AudioAsset
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance {get;private set;}

        [SerializeField] private AudioContainer audioContainer;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
    
        public AudioAsset GetAudioClip(AudioName audioName)
        {
            foreach(var data in audioContainer.audioData)
            {
                if(data.audioName == audioName) return data.audioAsset;
            }

            Debug.LogError($"MISSING !!! AUDIO ASSET NOT FOUND IN AUDIO CONTAINER......");
            return null;
        }

    }
}