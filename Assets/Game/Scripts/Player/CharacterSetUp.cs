using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreator2D;
using GS.FanstayWorld2D.UI;
using GS.FanstayWorld2D.Player;

namespace GS.FanstayWorld2D
{
    public class CharacterSetUp : MonoBehaviour
    {
        public CharacterViewer character;

       // private const string OUTFIT_PATH = ;

        private SelectedStoreData selectedStoreData;

        #region  Unity Func
        private void Start()
        {
            // character.LoadFromJSON("Assets/WHITE_GREEN.json");

            //Debug.Log($"HEX CODE {ColorUtility.ToHtmlStringRGB(Color.white)}");
        }

        private void OnEnable()
        {
            GameData.OnLoadData += OnStoreDataUpdate;
            GameData.OnSaveData += OnStoreDataUpdate;
        }

        private void OnDisable()
        {
            GameData.OnLoadData -= OnStoreDataUpdate;
            GameData.OnSaveData -= OnStoreDataUpdate;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                character.LoadFromJSON("Assets/BLACK_KNIGHT.json");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                character.LoadFromJSON("Assets/BLUE_GIRL.json");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                character.EquipPart(SlotCategory.MainHand, "Sword 01");
            }
        }

        #endregion
        private void OnStoreDataUpdate(SelectedStoreData data)
        {
            selectedStoreData = data;
            Debug.Log($"III {data.Outfit.name}");
            UpdateOutfit(data.Outfit.characterViwerData.Path);
            if (OnGamePlayUI.Instance != null) OnGamePlayUI.Instance.UpdateProfilePic(data.Outfit.generalInfo.protraitImg);

            UpdateWeapon(PlayerConstant.Instance.PlayerWeapon);
            // UpdatePlayerWeaponColor(data.Sword.generalInfo.itemType, data.Sword.characterViwerData);
            // UpdatePlayerWeaponColor(data.Bow.generalInfo.itemType, data.Bow.characterViwerData);
            // UpdatePlayerWeaponColor(data.Wand.generalInfo.itemType, data.Wand.characterViwerData);

            // Update Mermaid
        }

        #region Update Different parts

        public void UpdateWeapon(int weapon)
        {
            PlayerAnimation.Instance.SwitchWeapon(weapon);

            CharacterViwerData temp;
            switch (weapon)
            {
                case 0:
                    UpdateEquipePart(SlotCategory.MainHand, ""); // for empty hand
                    break;
                case 1:
                    temp = selectedStoreData.Sword.characterViwerData;
                    UpdateEquipePart(SlotCategory.MainHand, temp.Path);
                    UpdatePlayerWeaponColor(SlotCategory.MainHand, temp);
                    break;
                case 2:
                    temp = selectedStoreData.Bow.characterViwerData;
                    UpdateEquipePart(SlotCategory.OffHand, temp.Path);
                    UpdatePlayerWeaponColor(SlotCategory.OffHand, temp);
                    break;
                case 3:
                    temp = selectedStoreData.Wand.characterViwerData;
                    UpdateEquipePart(SlotCategory.MainHand, temp.Path);
                    UpdatePlayerWeaponColor(SlotCategory.MainHand, temp);
                    break;

            }
        }
        private void UpdateOutfit(string fileName)
        {
            Debug.Log($"{Application.streamingAssetsPath}/{fileName}.json");
            character.LoadFromJSON($"{Application.streamingAssetsPath}/{fileName}.json");
        }

        private void UpdateEquipePart(SlotCategory slotCategory, string partName)
        {
            character.EquipPart(slotCategory, partName);
        }


        private void UpdatePlayerWeaponColor(SlotCategory slotCategory, CharacterViwerData characterViwerData)
        {
            if (slotCategory == SlotCategory.MainHand || slotCategory == SlotCategory.OffHand)
            {
                UpdateColor(slotCategory, characterViwerData);
            }
        }
        private void UpdateColor(SlotCategory slotCategory, CharacterViwerData characterViwerData)
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
            if (itemType != ItemType.Outfit)
                return itemType == ItemType.Bow ? SlotCategory.OffHand : SlotCategory.MainHand;

            Debug.LogError($"Item Type Can't be ItemType.OUTFIT here");
            return SlotCategory.Nose;  // Dummy Value 
        }

        #endregion
    }
}