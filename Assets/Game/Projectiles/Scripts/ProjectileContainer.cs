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
        private int counter = -1;
        public ProjectileType projectileType;
        public List<GameObject> projectileObjs;

        public GameObject GetProjectile()
        {
            counter++;
            return projectileObjs[counter % projectileObjs.Count];
        }

    }
}