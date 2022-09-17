using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.CoomonLevelStuff
{
    public class LevelEndPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
            {
                PlayerController.Instance.CanInput = false;
                CutSceneCanvasScript.Instance.StartCutscene(()=>{PlayerController.Instance.AutoRun = true;});
            }
        }
    }
}