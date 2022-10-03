using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D.CoomonLevelStuff
{
    public class EndLevelScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Player"))
            {
                CutSceneCanvasScript.Instance.EndLevel(()=>{ SceneManager.LoadScene(0);});
            }
        }
    }
}
