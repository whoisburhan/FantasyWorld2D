using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GS.FanstayWorld2D
{
    [System.Serializable]
    public class SaveState
    {
        public int[] Outfits = new int[5] { 2, 0, 0, 0, 0 };

        public int[] Swords = new int[1] { 2 };
        public int[] Bow = new int[1] { 2 };
        public int[] Wand = new int[1] { 2 };
        public int[] Mermaid = new int[1] { 2 };

        //----------------------------------------------------------------------------
        public int[] Balls = new int[10] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] Grounds = new int[8] { 2, 0, 0, 0, 0, 0, 0, 0 };

        public int[] Puds = new int[38] { 1, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int HighScore { get; set; }

        public int SelectedLanguage { get; set; }

        public int TotalCoin { get; set; }

    }

    public class SelectedStoreData
    {
        public ItemData Outfit;
        public ItemData Sword;
        public ItemData Bow;
        public ItemData Wand;
        public ItemData MermaidOutfit;
    }

    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }

        public static Action<SelectedStoreData> OnLoadData, OnSaveData;
        public SaveState State { get => state; set => state = value; }

        [SerializeField] public StoreData storeData;

        public int CurrentlySelectedBallIndex = 0;
        public int CurrentlySelectedFieldIndex = 0;
        public int CurrentlySelectedPudIndex = 2;
        [Space]
        public int CurrentlySelectedOutfitIndex = 0;
        public int CurrentlySelectedSwordIndex = 0;
        public int CurrentlySelectedBowIndex = 0;
        public int CurrentlySelectedWandIndex = 0;
        public int CurrentlySelectedMermaidOutfitIndex = 0;
        private SelectedStoreData selectedStoreData = new SelectedStoreData();

        [Header("Logic")]

        [SerializeField] public string SaveFileName = "data.GS";
        private string saveFileName;
        [SerializeField] private bool loadOnStart = true;

        private SaveState state;
        private BinaryFormatter formatter;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            // state = new SaveState();
            // saveFileName = Application.persistentDataPath + "/" + SaveFileName;
            // Debug.Log(saveFileName);
            // formatter = new BinaryFormatter();
            // Load();
        }

        public void Save()
        {
            //If there no previous state loaded, create a new one
            if (State == null)
            {
                State = new SaveState();
            }

            var file = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(file, State);
            file.Close();

            UpdateStoreData();
            OnSaveData?.Invoke(selectedStoreData);
        }

        public void Load()
        {
            // Open a physical file, on your disk to hold the save


            try
            {
                // If we found the file, open and read it
                var file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
                State = (SaveState)formatter.Deserialize(file);
                file.Close();
                SelectedItemFinder();
                OnLoadData?.Invoke(selectedStoreData);
            }
            catch
            {
                Debug.Log("No file found, creating new entry...");
                state.TotalCoin = 2500;
                // UIManager.Instance.FirstTimeGameOn = true;
                Save();
            }
        }

        private void SelectedItemFinder()
        {
            CurrentlySelectedOutfitIndex = GetSelectedItemIndex(State.Outfits);
            CurrentlySelectedSwordIndex = GetSelectedItemIndex(State.Swords);
            CurrentlySelectedBowIndex = GetSelectedItemIndex(State.Bow);
            CurrentlySelectedWandIndex = GetSelectedItemIndex(State.Wand);
            CurrentlySelectedMermaidOutfitIndex = GetSelectedItemIndex(State.Mermaid);

            UpdateStoreData();
        }

        private int GetSelectedItemIndex(int[] itemList)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] == 2)
                    return i;
            }

            return 0;
        }

        private void UpdateStoreData()
        {
            selectedStoreData.Outfit = storeData.Outfits[CurrentlySelectedOutfitIndex];
            selectedStoreData.Sword = storeData.Swords[CurrentlySelectedSwordIndex];
            selectedStoreData.Bow = storeData.Bows[CurrentlySelectedBowIndex];
            selectedStoreData.Wand = storeData.Wands[CurrentlySelectedWandIndex];
            selectedStoreData.Outfit = storeData.Mermaids[CurrentlySelectedMermaidOutfitIndex];
        }

    }
}