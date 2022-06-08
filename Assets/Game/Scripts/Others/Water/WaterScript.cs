using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.Extras
{
    public class WaterScript : MonoBehaviour
    {
        [SerializeField] private BuoyancyEffector2D buoyancyEffector2D;
        [SerializeField] private GameObject mermaidModeCollider;

        private void OnEnable()
        {
            PlayerConstant.OnCharacterChange += UpdateWaterPhysics;
        }

        private void OnDisable()
        {
            PlayerConstant.OnCharacterChange -= UpdateWaterPhysics;
        }

        private void UpdateWaterPhysics()
        {
            if(PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
            {
                mermaidModeCollider.SetActive(true);
                buoyancyEffector2D.enabled = false;
            }
            else
            {
                mermaidModeCollider.SetActive(false);
                buoyancyEffector2D.enabled = true;
            }
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (!PlayerConstant.Instance.CanSwime)
                {
                    Debug.Log("PlayerConstant.Instance.CanSwime");
                    PlayerConstant.Instance.CanSwime = true;
                   // PlayerConstant.Instance.UpdateCollider();
                }
            }
        }

        
    }
}