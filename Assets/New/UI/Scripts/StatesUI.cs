using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GS.FanstayWorld2D.UI
{
    public class StatesUI : UIPanel
    {
        #region  States
        [Header("States")]
        [SerializeField] private Text HP_Text;
        [SerializeField] private Text MP_Text;
        [SerializeField] private Text ArrowText;
        [SerializeField] private Text CoinText;
        [SerializeField] private Text CurrentSessionTimeText;
        [SerializeField] private Text GamePlayTimeText;
        #endregion

        #region  Active Character Outfits
        [Header("Activa Character Outfits")]
        [SerializeField] private Image humanCharacterImg;
        [SerializeField] private Image mermaidCharacterImg;
        #endregion

        #region  Active Waeapon & Skills
        [Header("Active Weapon & Skill")]
        [SerializeField] private Image meleeWeaponImg;
        [SerializeField] private Image swordWeaponImg;
        [SerializeField] private Image bowWeaponImg;
        [SerializeField] private Image wandWeaponImg;
        [SerializeField] private Image mermaidImg;
        [SerializeField] private Image mermaidAttackSpellImg;
        #endregion

        private void Start()
        {
            UpdateHealthUI(20, 200);
            UpdateCurrentSessionTimeInUI(512);
        }

       

        #region Updates States
        private void UpdateHealthUI(int currentHealth, int maxHealth)
        {
            HP_Text.text = $"HP : {currentHealth}/{maxHealth}";
        }

        private void UpdateMagicPointUI(int currentMagicPoint, int maxMagicPoint)
        {
            MP_Text.text = $"MP : {currentMagicPoint}/{maxMagicPoint}";
        }

        private void UpdateArrowCountUI(int currentArrows, int maxArrowsCanHold)
        {
            ArrowText.text = $"ARROWS : {currentArrows}/{maxArrowsCanHold}";
        }

        private void UpdateCoinInUI(int coinAmount)
        {
            CoinText.text = $"COIN : {coinAmount}";
        }

        private void UpdateCurrentSessionTimeInUI(int timeInseconds)
        {
            CurrentSessionTimeText.text = $"CURRENT SESSION : {SecondsToString(timeInseconds)}";
        }

        private void UpdateTotalGamePlayTimeInUI(int timeInseconds)
        {
            GamePlayTimeText.text = $"GAMEPLAY TIME : {SecondsToString(timeInseconds)}";
        }
        private string SecondsToString(int timeInseconds)
        {
            string seconds = timeInseconds % 60 < 10 ? "0" + $"{timeInseconds % 60}" : $"{timeInseconds % 60}";
            string minutes = (timeInseconds /60) % 60 < 10 ? "0" + $"{(timeInseconds /60) % 60}" : $"{(timeInseconds /60) % 60}";
            string hours = timeInseconds / 3600  < 10 ? "0" + $"{timeInseconds / 3600}":$"{timeInseconds / 3600}";

            return $"{hours}:{minutes}:{seconds}";
        }
        #endregion
    
        #region  Update Active Character Outfits

        private void UpdateHumanCharacterOutfitInUI(Sprite outfit)
        {
            humanCharacterImg.sprite = outfit;
        }

        private void UpdateMermaidCharacterOutfitInUI(Sprite outfit)
        {
            mermaidCharacterImg.sprite = outfit;
        }

        #endregion
    
        #region Update Active Weapon & Skills

        private void UpdateActiveMeleeWeaponUI(Sprite weapon)
        {
            meleeWeaponImg.sprite = weapon;
        }

        private void UpdateActiveSwordWeaponUI(Sprite weapon)
        {
            swordWeaponImg.sprite = weapon;
        }

        private void UpdateActiveBowWeaponUI(Sprite weapon)
        {
            bowWeaponImg.sprite = weapon;
        }

        private void UpdateActiveWandWeaponUI(Sprite weapon)
        {
            wandWeaponImg.sprite = weapon;
        }

        private void UpdateMermaidAttackSpellInUI(Sprite weapon, Sprite mermaid)
        {
            mermaidImg.sprite = mermaid;
            mermaidAttackSpellImg.sprite = weapon;
        }
        

        #endregion
    }
}