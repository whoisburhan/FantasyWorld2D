using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private void Update()
        {
            if (PlayerConstant.Instance.CanSwime && PlayerController.Instance.Switch_Character)
            {
                if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                {
                    PlayerConstant.Instance.GamePlayerType = PlayerType.Mermaid;
                    
                }
                else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                {
                    PlayerConstant.Instance.GamePlayerType = PlayerType.Human;
                }
            }
        }
    }
}