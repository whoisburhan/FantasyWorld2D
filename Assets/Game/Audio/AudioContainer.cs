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
        LANDED_2,
        JUMP_INTO_WATER,
        OUCH,
        OUCH_2,
        OUCH_3,
        OUCH_4,
        OUCH_5,
        OUCH_6,
        DIE,
        WATER
        
    }

    [Serializable]
    public class AudioData 
    {
        public AudioName audioName;
        public AudioAsset audioAsset;
    }
}