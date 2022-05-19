using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreator2D;

namespace GS.FanstayWorld2D.Player
{
    public enum ActionMaps
    {
        Land, Water, UI
    }

    public class PlayerConstant : MonoBehaviour
    {
        public static PlayerConstant Instance { get; private set; }

        public ActionMaps CurrentActionMap { get; private set; }

        [SerializeField] private Rigidbody2D playerRigidbodyMain;
        [SerializeField] private CharacterViewer characterviewer;
        public bool IsPlayerDead = false;
        public bool CanClimb
        {
            get { return canClimb; }
            set
            {
                canClimb = value;
                playerRigidbodyMain.isKinematic = canClimb ? true : false;
                playerRigidbodyMain.velocity = Vector2.zero;
                CanAttack = !canClimb;
            }
        }
        private bool canClimb = false;

        public bool CanAttack = true;

        public string[] WeaponList = new string[4]{"","Sword 01","Bow 01","Wand 01"};

        public int PlayerWeapon
        {
            get { return playerWeapon; }
            set
            {
                playerWeapon = value;
                if (playerWeapon >= 4)
                    playerWeapon = 0;
                PlayerAnimation.Instance.SwitchWeapon(playerWeapon);
                characterviewer.EquipPart(playerWeapon == 2 ? SlotCategory.OffHand : SlotCategory.MainHand,WeaponList[playerWeapon]);

            }
        }

        private int playerWeapon = 0;

        [Header("Particles")]
        public ParticleSystem JumpParticles, LaunchParticles;
        public ParticleSystem MoveParticles, LandParticles;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void SwitchCurrentActionMap(ActionMaps actionMap)
        {
            CurrentActionMap = actionMap;
        }

    }
}