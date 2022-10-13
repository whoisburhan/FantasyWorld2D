using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GS.FanstayWorld2D.UI
{
    public class StoreUI : UIPanel
    {
        public static Action<ItemData> OnUpdateDetailsPanel;

        #region Item Details
        [Header("Item Details")]
        [SerializeField] private Text itemNameText;
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
        #endregion

        #region Item Slots

        [Header("ItemSlots")]
        private ShopItemSlot[] itemSlots;

        #endregion

        #region Unity Func
        private void Start()
        {
           itemSlots = GetComponentsInChildren<ShopItemSlot>(true);
        }

        private void OnEnable()
        {
            OnUpdateDetailsPanel += UpdateDetailsPanel;
        }

        private void OnDisable()
        {
            OnUpdateDetailsPanel -= UpdateDetailsPanel;
        }
        #endregion

        #region  Item Details Func

        private void UpdateDetailsPanel(ItemData itemData)
        {
            UpdateItemName(itemData.generalInfo.Name);
            UpdatePrice(itemData.shopData.Price);
            UpdateItemStatus(itemData.shopData.itemStatus);
            UpdateAttackText(itemData.gameBenifits.Attack);
            UpdateDisplayImg(itemData.generalInfo.displayImg);
            UpdateBuyOrEquipeButtonAppearance(itemData.shopData.itemStatus);
        }

        private void UpdateItemName(string itemName)
        {
            itemNameText.text = itemName;
        }
        private void UpdatePrice(int price)
        {
            priceText.text = $"PRICE : {price}";
        }

        private void UpdateItemStatus(ItemStatus itemStatus)
        {
            switch(itemStatus)
            {
                case ItemStatus.NOT_OWNED:
                    statusText.text = "NOT OWNED";
                    statusText.color = Color.red;
                    break;
                case ItemStatus.OWNED:
                    statusText.text = "OWNED";
                    statusText.color = Color.green;
                    break;
                case ItemStatus.EQUIPED:
                    statusText.text = "Equiped";
                    statusText.color = Color.green;
                    break;

            }
        }

        private void UpdateAttackText(int attackValue)
        {
            attackText.text = $"ATK : {attackValue}";
        }

        private void UpdateDisplayImg(Sprite display)
        {
            displayImg.sprite = display;
        }

        private void UpdateBuyOrEquipeButtonAppearance(ItemStatus itemStatus)
        {
            switch(itemStatus)
            {
                case ItemStatus.NOT_OWNED:
                    buyOrEquipeButtonImg.sprite = greenButtonSprite;
                    buyOrEquipeButtonText.text = "BUY";
                    break;

                case ItemStatus.OWNED:
                    buyOrEquipeButtonImg.sprite = greenButtonSprite;
                    buyOrEquipeButtonText.text = "EQUIP";
                    break;

                case ItemStatus.EQUIPED:
                    buyOrEquipeButtonImg.sprite = goldenButtonSprite;
                    buyOrEquipeButtonText.text = "EQUIPED";
                    break;
            }
        }
        #endregion
    

        #region  Update Shop Slots

        private void UpdateShopSlots(List<ItemData> items)
        {
            for(int i = 0; i < itemSlots.Length ; i++)
            {
                itemSlots[i].Init(items.Count > i ? items[i] : null);
            }
        }

        #endregion

    }
}