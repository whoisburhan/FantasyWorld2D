using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GS.FanstayWorld2D.ConsumableItem
{
    public enum ConsumableTiles
    {
        NONE
    }

    public class ConsumableContainer : MonoBehaviour
    {
        public static ConsumableContainer Instance { get; private set; }

        [SerializeField] private List<ConsumableTileInfo> consumableTileInfo;

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
    
        public Tile GetConsumableTile(ConsumableTiles consumableTiles)
        {
            if(consumableTiles == ConsumableTiles.NONE) return null;
            
            foreach(var item in consumableTileInfo)
            {
                if(item.consumableTilesType == consumableTiles)
                {
                    return item.tile;
                }
            }
            return null;
        }

    }

    [Serializable]
    public class ConsumableTileInfo
    {
        public ConsumableTiles consumableTilesType;
        public Tile tile;
    }
}