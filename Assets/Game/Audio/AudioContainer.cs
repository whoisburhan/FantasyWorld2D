using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GS.AudioAsset
{
    [CreateAssetMenu(fileName = "Audio Container", menuName = "GS/Audio Container", order = 4)]
    public class AudioContainer : ScriptableObject
    {
        public List<AudioData> audioData;
    }

    public enum AudioName
    {
        SWORD_SLASH,
        SWORD_SLASH_3,
        JUMP,
        JUMP_2,
        LANDED,
        LANDED_2
    }

    [Serializable]
    public class AudioData 
    {
        public AudioName audioName;
        public AudioAsset audioAsset;
    }
}