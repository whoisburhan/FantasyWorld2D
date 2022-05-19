using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class ClimbChecker : MonoBehaviour
    {
        [SerializeField] private float handOffest = 0.82f;
        private void OnTriggerEnter2D(Collider2D col)
        {
            Transform parent = transform.parent;
            Debug.Log("OK");
            if (col.CompareTag("Climable"))
            {
                PlayerConstant.Instance.CanClimb = true;
                
                parent.position = new Vector3(parent.transform.localScale.x >= 0f ? col.transform.position.x - handOffest : col.transform.position.x + handOffest, parent.transform.position.y);
            }

            if(col.CompareTag("UpLift"))
            {
                parent.position = new Vector3(parent.position.x , col.transform.parent.position.y );
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Climable"))
            {
                 PlayerConstant.Instance.CanClimb = false;
            }
        }
    }
}