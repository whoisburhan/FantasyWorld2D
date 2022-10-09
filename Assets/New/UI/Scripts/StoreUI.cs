using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GS.FanstayWorld2D.UI
{
    public class StoreUI : UIPanel
    {
       [Header("Item Details")]
       [SerializeField] private Text priceText;
       [SerializeField] private Text statusText;
       [SerializeField] private Text attackText;
       [Space]
       [SerializeField] private Image displayImg;
       [Space]
       [SerializeField] private Button buyOrEquipeButton;
       [SerializeField] private Image buyOrEquipeButtonImg;
       [SerializeField] private Text buyOrEquipeButtonText;
       [Space]
       [SerializeField] private Sprite greenButtonSprite;
       [SerializeField] private Sprite goldenButtonSprite;
       
        
    }
}