using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreator2D;

namespace GS.FanstayWorld2D
{
    public class CharacterSetUp : MonoBehaviour
    {
        public CharacterViewer character;

        private void Start()
        {
            character.LoadFromJSON("Assets/WHITE_GREEN.json");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                character.LoadFromJSON("Assets/WHITE_GREEN.json");
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                character.LoadFromJSON("Assets/RED_BLACK.json");
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                character.EquipPart(SlotCategory.MainHand,"Sword 01");
            }
        }
    }
}