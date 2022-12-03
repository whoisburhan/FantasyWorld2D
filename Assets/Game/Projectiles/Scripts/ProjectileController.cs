using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        public static ProjectileController Instance { get; private set; }
        [Header("Projectile Samples")]
        [SerializeField] private List<ProjectileInfo> container;
        [Header("Particles Samples")]
        [SerializeField] private List<ParticleInfo> particleContainer;

        private ProjectileType activeProjectileType;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void SetActiveProjectile(ProjectileType type, int power)
        {
            activeProjectileType = type;
            SetProjectilePower(activeProjectileType,power);

        }
        public GameObject GetActiveProjectile()
        {
            return GetProjectile(activeProjectileType);
        }
        
        private void SetProjectilePower(ProjectileType type, int power)
        {
            foreach(var projectile in container)
            {
                if(projectile.projectileType == type)
                {
                    projectile.SetProjectilePower(power);
                    return;
                }
            }
        }
        private GameObject GetProjectile(ProjectileType type)
        {
            foreach(var projectile in container)
            {
                if(projectile.projectileType == type)
                {
                    return projectile.GetProjectile();
                }
            }

            Debug.LogError($"Projector type {type} is not found in the container");
            return null;
        }

        public GameObject GetParticle(ParticleType type)
        {
            foreach(var particle in particleContainer)
            {
                if(particle.particleType == type)
                {
                    return particle.GetParticle();
                }
            }

            Debug.LogError($"Particle type {type} is not found in the container");
            return null;
        }
    }

    public enum ProjectileType
    {
        None = -1, ICE, FIRE, WATER, COMET, FIRE_2, LIGHTNING
    }

    public enum ParticleType
    {
        COIN
    }
}