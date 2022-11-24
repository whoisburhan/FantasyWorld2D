using System.Security.Claims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.UI
{
    public interface IPanel
    {
        public void ShowPanel();
        public void HidePanel();
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour, IPanel
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        #region Hide & Show Panel
        public void ShowPanel()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        public void HidePanel()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        #endregion
    }
}