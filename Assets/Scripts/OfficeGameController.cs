using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts
{
    public class OfficeGameController : MonoBehaviour
    {
        [SerializeField] private List<Level> _gameLevels;
        private List<LevelStatus> _saveGame;
        public string PathFile = "saveGame.bin";

        // Use this for initialization
        void Start ()
        {
            if (!Load()) UnlockFirstLevel();
        }

        private void UnlockFirstLevel()
        {
            var firstDoor = _gameLevels.FirstOrDefault();
            if (firstDoor != null) firstDoor.levelDoor.SetUnlock(true);
        }

        private void SimulateGame()
        {
            var random = new System.Random();
            
            for (int i = 0; i < Convert.ToInt32(Random.Range(2, 9)); i++)
            {
                _gameLevels[random.Next(2)].recordPunctuations.Add(new Punctuation(Convert.ToInt64(Random.Range(0.5f, 2.5f)), Convert.ToInt32(Random.Range(0, 2500))));
            }
            
        }

        public void Save()
        {
            List<LevelStatus> statusList = new List<LevelStatus>();
            foreach (var gameLevel in _gameLevels)
            {
                statusList.Add(new LevelStatus(gameLevel));
            }
            _saveGame = statusList;
            BinaryFileManager.Save(PathFile,_saveGame);
        }

        private bool Load()
        {
            _saveGame = BinaryFileManager.Load(PathFile);
            var savedLevels = _saveGame;
            if (savedLevels == null || _gameLevels == null) return false;

            for (int i = 0; i < savedLevels.Count; i++)
            {
                RestoreLevel(savedLevels[i], _gameLevels[i]);
            }
            return true;
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
