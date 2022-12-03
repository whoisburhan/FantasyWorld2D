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
        SWORD_SLASH = 10, SWORD_SLASH_3, ARROW_SHOT,
        JUMP = 20, JUMP_2,
        LANDED = 30, LANDED_2,
        JUMP_INTO_WATER = 40,
        OUCH = 50, OUCH_2, OUCH_3, OUCH_4, OUCH_5, OUCH_6,
        DIE = 60,
        WATER = 70,
        CAVE = 80,
        ENEMY_DEATH = 90, ENEMY_DEATH_2, ENEMY_DEATH_3, ENEMY_DEATH_4, ENEMY_DEATH_5,
        PROJECTILE_WATER = 100, PROJECTILE_FIRE, PROJECTILE_ICE,
        COIN = 110,
        UI_CLICK = 120, UI_EQUIPE
    }

    [Serializable]
    public class AudioData
    {
        public AudioName audioName;
        public AudioAsset audioAsset;
    }
}