using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GS.FanstayWorld2D.CoomonLevelStuff
{
    public class CutSceneCanvasScript : MonoBehaviour
    {

        public static CutSceneCanvasScript Instance {get;private set;}

        [SerializeField] private float cutSceneAnimationBarDuration = 1f;
        [SerializeField] private float levelChangeAnimationBarDuration = 1f;

        [Header("Top Bar")]
        [SerializeField] private Transform topPanel;
        [SerializeField] private Transform topPanelDefaultOffset;
        [SerializeField] private Transform topPanelCutsceneOffset;
        [SerializeField] private Transform topPanelLevelOffset;
        [Header("Bottom Bar")]
        [SerializeField] private Transform bottomPanel;
        [SerializeField] private Transform bottomPanelDefaultOffset;
        [SerializeField] private Transform bottomPanelCutsceneOffset;
        [SerializeField] private Transform bottomPanelLevelOffset;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        public void StartCutscene(Action action = null)
        {
            topPanel.DOMove(topPanelCutsceneOffset.position, cutSceneAnimationBarDuration).OnComplete(()=>{action?.Invoke();});
            bottomPanel.DOMove(bottomPanelCutsceneOffset.position, cutSceneAnimationBarDuration);
        }

        public void EndCutScene(Action action = null)
        {
            topPanel.DOMove(topPanelDefaultOffset.position, cutSceneAnimationBarDuration).OnComplete(()=>{action?.Invoke();});;
            bottomPanel.DOMove(bottomPanelDefaultOffset.position, cutSceneAnimationBarDuration);
        }

        public void StartLevel(Action action = null)
        {
            topPanel.DOMove(topPanelLevelOffset.position, cutSceneAnimationBarDuration).OnComplete(()=>{action?.Invoke();});;
            bottomPanel.DOMove(bottomPanelLevelOffset.position, cutSceneAnimationBarDuration);
        }

        public void EndLevel(Action action = null)
        {
            topPanel.DOMove(topPanelDefaultOffset.position, cutSceneAnimationBarDuration).OnComplete(()=>{action?.Invoke();});;
            bottomPanel.DOMove(bottomPanelDefaultOffset.position, cutSceneAnimationBarDuration);
        }
    }
}