using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreator2D;
using System;

namespace GS.FanstayWorld2D.Player
{
    public enum ActionMaps
    {
        Land, Water, UI
    }

    public enum PlayerType
    {
        Human = 0, Mermaid, Cursed_Ball
    }

    public class PlayerConstant : MonoBehaviour
    {
        public static PlayerConstant Instance { get; private set; }

        public ActionMaps CurrentActionMap { get; private set; }

        public static Action OnCharacterChange;

        [SerializeField] private Rigidbody2D playerRigidbodyMain;
        [SerializeField] private CharacterViewer characterviewer;

        private CharacterSetUp characterSetUp;

        [Header("CharacterController")]
        [SerializeField] private GameObject humanCharacter;
        [SerializeField] private GameObject mermaidCharacter;
        [Header("Human Player Colliders")]
        [SerializeField] private Transform inLandMainCharacterColiderObj;
        [SerializeField] private Transform inWaterMainCharacterColiderObj;

        [Header("Mermaid Player Colliders")]
        public GameObject MermaidColliderIdle;
        public GameObject MermaidColliderRun;
        [Space]
        public bool IsPlayerDead = false;

        public bool CanSwime
        {
            get { return canSwime; }
            set
            {
                canSwime = value;
                PlayerAnimation.Instance.CanSwime(canSwime);
                inLandMainCharacterColiderObj.gameObject.SetActive(!canSwime);
                inWaterMainCharacterColiderObj.gameObject.SetActive(canSwime);
            }
        }

        private bool canSwime = false;

        public PlayerType GamePlayerType
        {
            get { return playerType; }
            set
            {
                playerType = value;

                if (value == PlayerType.Human)
                {
                    //humanCharacter.SetActive(true);
                    characterviewer.TintColor = new Color(characterviewer.TintColor.r, characterviewer.TintColor.g, characterviewer.TintColor.b, 1f);
                    mermaidCharacter.SetActive(false);
                    CanSwime = true;
                }
                else if (value == PlayerType.Mermaid)
                {
                    PlayerAnimation.Instance.CanSwime(false);
                    mermaidCharacter.SetActive(true);
                    characterviewer.TintColor = new Color(characterviewer.TintColor.r, characterviewer.TintColor.g, characterviewer.TintColor.b, 0f);
                    inLandMainCharacterColiderObj.gameObject.SetActive(false);
                    inWaterMainCharacterColiderObj.gameObject.SetActive(false);
                   // humanCharacter.SetActive(false);
                }

                OnCharacterChange?.Invoke();
            }
        }

        private PlayerType playerType;

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

        public string[] WeaponList = new string[4] { "", "Sword 01", "Bow 01", "Wand 01" };

        private SelectedStoreData selectedStoreData;

        private int playerWeapon = 0;
        public int PlayerWeapon
        {
            get { return playerWeapon; }
            set
            {
                playerWeapon = value;
                if (playerWeapon >= 4)
                    playerWeapon = 0;
                PlayerAnimation.Instance.SwitchWeapon(playerWeapon);
                characterSetUp.UpdateWeapon(playerWeapon);

            }
        }

        private void UpdateWeapon(int playerWeapon)
        {
            
            characterviewer.EquipPart(playerWeapon == 2 ? SlotCategory.OffHand : SlotCategory.MainHand, WeaponList[playerWeapon]);
        }
        private void GetActiveWeaponDetails(SelectedStoreData data)
        {
            selectedStoreData = data;
        }

        [Header("Particles")]
        public ParticleSystem JumpParticles, LaunchParticles;
        public ParticleSystem MoveParticles, LandParticles;
        public ParticleSystem BloodParticles;


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

            characterSetUp = GetComponent<CharacterSetUp>();
        }

        public void SwitchCurrentActionMap(ActionMaps actionMap)
        {
            CurrentActionMap = actionMap;
        }



    }
}