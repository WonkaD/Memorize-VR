using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts
{
    public class OfficeGameController : MonoBehaviour
    {
        [SerializeField] private List<Level> GameLevels;
        private List<LevelStatus> SaveGame;
        public string PathFile = "saveGame.xml";

        // Use this for initialization
        void Start ()
        {
            Load();
            SimulateGame();
            Save();

        }

        private void SimulateGame()
        {
            var random = new System.Random();
            
            for (int i = 0; i < Convert.ToInt32(Random.Range(2, 9)); i++)
            {
                GameLevels[random.Next(2)].recordPunctuations.Add(new Punctuation(Convert.ToInt64(Random.Range(0.5f, 2.5f)), Convert.ToInt32(Random.Range(0, 2500))));
            }
            
        }

        public void Save()
        {
            List<LevelStatus> statusList = new List<LevelStatus>();
            foreach (var gameLevel in GameLevels)
            {
                statusList.Add(new LevelStatus(gameLevel));
            }
            SaveGame = statusList;
            GameFileManager.Save(PathFile, SaveGame);
        }

        private void Load()
        {
            SaveGame = GameFileManager.Load(PathFile);
            var savedLevels = SaveGame;
            if (savedLevels == null || GameLevels == null) return;

            for (int i = 0; i < savedLevels.Count; i++)
            {
                RestoreLevel(savedLevels[i], GameLevels[i]);
            }
        }

        private static void RestoreLevel(LevelStatus savedLevel, Level gameLevel)
        {
            gameLevel.recordPunctuations = savedLevel.Punctuations;
            gameLevel.levelDoor.SetUnlock(savedLevel.Unlock);
        }

        public void Finish()
        {
            Save();
        }
	
        // Update is called once per frame
        void Update () {
		
        }

    }
}
