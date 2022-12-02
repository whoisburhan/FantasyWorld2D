using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Enemy
{
    public class Trape : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.layer == 7 || col.gameObject.layer == 8)
            {
                col.gameObject.SendMessage("Damage",int.MaxValue);
            }
        }
    }
}