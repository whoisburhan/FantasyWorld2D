using UnityEngine;
using UnityEngine.InputSystem;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        private PlayerControls playerControls;

        [HideInInspector] public float MoveX, MoveY;
        [HideInInspector] public bool Sprint, Jump, ReleaseJump, SwitchWeapon,Attack_1;

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

            playerControls = new PlayerControls();
        }


        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            switch (PlayerConstant.Instance.CurrentActionMap)
            {
                case ActionMaps.Land:
                    ActionMap_Land();
                    break;
                case ActionMaps.Water:
                    ActionMap_Water();
                    break;
                case ActionMaps.UI:
                    ActionMap_UI();
                    break;
            }
        }

        private void ActionMap_Land()
        {
            Jump = playerControls.Land.Jump.WasPressedThisFrame();
            ReleaseJump = playerControls.Land.Jump.WasReleasedThisFrame();

            SwitchWeapon = playerControls.Land.Switch_Weapon.WasPressedThisFrame();

            if (!PlayerConstant.Instance.CanClimb)
            {
                MoveX = playerControls.Land.Move_X.ReadValue<float>();
                Sprint = playerControls.Land.Sprint.inProgress;

            }
            else
            {
                MoveY = playerControls.Land.Move_Y.ReadValue<float>();
            }

            if(PlayerConstant.Instance.CanAttack)
            {
                Attack_1 = playerControls.Land.Attack_1.WasPressedThisFrame();
            }
        }

        private void ActionMap_Water()
        {

        }

        private void ActionMap_UI()
        {

        }
    }
}