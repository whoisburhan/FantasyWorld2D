using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        public static ProjectileController Instance { get; private set; }
        [Header("Projectile Samples")]
        [SerializeField] private ProjectileContainer container;
        public GameObject ICE;

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
    }

    public enum ProjectileType
    {
        ICE
    }
}