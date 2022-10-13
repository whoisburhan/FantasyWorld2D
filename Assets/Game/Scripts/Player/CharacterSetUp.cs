using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreator2D;

namespace GS.FanstayWorld2D
{
    public class CharacterSetUp : MonoBehaviour
    {
        public CharacterViewer character;
        private Color a = Color.white;

        private void Start()
        {
            character.LoadFromJSON("Assets/WHITE_GREEN.json");
            
             Debug.Log($"HEX CODE {ColorUtility.ToHtmlStringRGB(Color.white)}");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                character.LoadFromJSON("Assets/WHITE_GREEN.json");
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                character.LoadFromJSON("Assets/BLUE_GIRL.json");
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                character.EquipPart(SlotCategory.MainHand,"Sword 01");
            }
        }

        #region Update Different parts

        private void UpdateOutfit(string outfitPath)
        {
            character.LoadFromJSON(outfitPath);
        }

        private void UpdateEquipePart(SlotCategory slotCategory, string partName)
        {
            character.EquipPart(slotCategory,partName);
        }


        private void UpdatePlayerWeaponColor(ItemType itemType, CharacterViwerData characterViwerData)
        {
            SlotCategory slotCategory = GetSlotCategory(itemType);

            if(slotCategory == SlotCategory.MainHand || slotCategory == SlotCategory.OffHand)
            {
                UpdateColor(slotCategory, characterViwerData);
            }
        }
        private void UpdateColor(SlotCategory slotCategory,CharacterViwerData characterViwerData)
        {
            UpdatePartColor(slotCategory, ColorCode.Color1, characterViwerData.Color1);
            UpdatePartColor(slotCategory, ColorCode.Color2, characterViwerData.Color2);
            UpdatePartColor(slotCategory, ColorCode.Color3, characterViwerData.Color3);
        }

        // Update Part Color
        private void UpdatePartColor(SlotCategory slotCategory, string colorCode, Color color)
        {
            character.SetPartColor(slotCategory, colorCode, color);
        }

        private SlotCategory GetSlotCategory(ItemType itemType)
        {
            if(itemType != ItemType.Outfit)
                return itemType == ItemType.Bow ? SlotCategory.OffHand : SlotCategory.MainHand;
            
            Debug.LogError($"Item Type Can't be ItemType.OUTFIT here");
            return SlotCategory.Nose;  // Dummy Value 
        }

        #endregion
    }
}