using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GS.AudioAsset
{
    [CreateAssetMenu(fileName = "Audio Asset", menuName = "GS/Audio Asset", order = 3)]
    public class AudioAsset : ScriptableObject
    {
        public string FileName;
        public AudioClip Clip;
        public string Source;
        [TextArea]
        public string Description;
    }
}