using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Extras
{
    public class BlockDetector : MonoBehaviour
    {
        [SerializeField] private Color blockDetectorColor;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 size;
        [SerializeField] private LayerMask maskLayer;
        [SerializeField] private List<int> neighbourBlocks;

        private bool activateNeighbourBlocks = false;

        private void OnDisable()
        {
            activateNeighbourBlocks = false;
        }

        private void Update()
        {
            Collider2D[] detetectObj = Physics2D.OverlapBoxAll( (Vector2)transform.position + offset, size, 0f, maskLayer);
            
            if(detetectObj.Length > 0 && !activateNeighbourBlocks)
            {
                // Activate all the neighbour blocks
                activateNeighbourBlocks = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = blockDetectorColor;
            Gizmos.DrawWireCube(transform.position + (Vector3)offset, size);

            GUIStyle style = new GUIStyle();
            style.fontSize = 20;

            #if UNITY_EDITOR
            UnityEditor.Handles.Label(transform.position + (Vector3)offset, gameObject.name, style);
            #endif
        }
    }
}