using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Extras
{
    public class BlockManager : MonoBehaviour
    {
        public static BlockManager Instance {get;private set;}

        [SerializeField] private List<GameObject> allBlocks;

        private List<int> activeBlockList;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    
        public void RefreshActiveBlockList(List<int> blockList)
        {
            foreach(int block in blockList)
            {
                if(!activeBlockList.Contains(block))
                {
                    allBlocks[block].SetActive(true);
                }
            }

            foreach(int block in activeBlockList)
            {
                if(!blockList.Contains(block))
                {
                    allBlocks[block].SetActive(false);
                }
            }
            
            activeBlockList = blockList;
        }

    
    }
}