using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy_UnderWater
{
    public class ScrollableBackgroundDetector : MonoBehaviour
    {
        [SerializeField] private ScrollingBackgroundScript scrollingBackgroundScript;

        [SerializeField] private Transform player;
        private float offestX;
        private void Start()
        {
            offestX = player.transform.position.x - transform.position.x;
        }
        private void LateUpdate()
        {
            transform.position = new Vector3(player.transform.position.x - offestX, transform.position.y);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Respawn"))
            {
                scrollingBackgroundScript.RePositionChild();
            }

            // if (col.CompareTag("Bomb"))
            // {
            //     col.gameObject.SetActive(false);
            // }
        }
    }
}