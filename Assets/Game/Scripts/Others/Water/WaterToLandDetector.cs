using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.Extras
{
    public class WaterToLandDetector : MonoBehaviour
    {
        [SerializeField] private Transform repositionPoint;
        [SerializeField] private Transform repositionPointInWater;
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (PlayerConstant.Instance.CanSwime)
                {
                    PlayerConstant.Instance.transform.position = repositionPoint.position;
                    PlayerConstant.Instance.CanSwime = false;
                }
                else
                {
                    PlayerConstant.Instance.transform.position = repositionPointInWater.position;
                    PlayerConstant.Instance.CanSwime = true;
                }
            }
        }
    }
}