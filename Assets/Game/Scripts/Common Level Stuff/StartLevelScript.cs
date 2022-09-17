using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.CoomonLevelStuff
{
    public class StartLevelScript : MonoBehaviour
    {
        private void Start()
        {
            PlayerController.Instance.CanInput = false;
            PlayerController.Instance.AutoRun = false;

            CutSceneCanvasScript.Instance.StartLevel(()=>
            {
                CutSceneCanvasScript.Instance.StartCutscene(()=>
                {
                    PlayerController.Instance.AutoRun = true;
                });
            });
        }
    }
}