using System;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Projectile
{
    [CreateAssetMenu(fileName = "Projectile Container", menuName = "GS/Projectile Container", order = 5)]
    public class ProjectileContainer : ScriptableObject
    {
        List<ProjectileInfo> projectiles;
    }

    [Serializable]
    public class ProjectileInfo
    {
        public ProjectileType projectileType;
        public GameObject projectileObj;
    }
}