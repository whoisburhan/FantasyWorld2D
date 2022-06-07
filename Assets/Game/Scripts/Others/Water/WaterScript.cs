using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.Extras
{
    public class WaterScript : MonoBehaviour
    {
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

        // public void OnTriggerExit2D(Collider2D col)
        // {
        //     if (col.CompareTag("Player"))
        //     {
        //         if (PlayerConstant.Instance.CanSwime)
        //         {
        //             Debug.Log("PlayerConstant.Instance.CanSwime Exit");
        //             PlayerConstant.Instance.CanSwime = false;
        //            // PlayerConstant.Instance.UpdateCollider();
        //         }
        //     }
        // }
    }
}