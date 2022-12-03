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
        public List<Projectile> projectileObjs;

        public void SetProjectilePower(int power)
        {
            foreach(var projectile in projectileObjs)
            {
                projectile.UpdateAtkPower(power);
            }
        }

        public GameObject GetProjectile()
        {
            counter++;
            projectileObjs[counter % projectileObjs.Count].gameObject.SetActive(true);
            return projectileObjs[counter % projectileObjs.Count].gameObject;
        }

    }

    [Serializable]
    public class ParticleInfo
    {
        private int counter = -1;
        public ParticleType particleType;
        public List<GameObject> particleObjs;

        public GameObject GetParticle()
        {
            counter++;
            return particleObjs[counter % particleObjs.Count];
        }

    }
}