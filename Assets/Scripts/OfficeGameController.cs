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
            if (firstDoor != null) firstDoor.LevelDoor.SetUnlock(true);
        }

        private void Save()
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
            gameLevel.RecordPunctuations = savedLevel.Punctuations;
            gameLevel.LevelDoor.SetUnlock(savedLevel.Unlock);
        }

        public void Finish()
        {
            Save();
        }

        public void StartLevel(int levelIndex)
        {

            _gameLevels[levelIndex].LevelDoor.CloseDoorAndLock();
        }
        public void FinishLevel(int levelIndex, Punctuation punctuation)
        {
            var level = _gameLevels[levelIndex];
            level.RecordPunctuations.Add(punctuation);
            level.LevelDoor.OpenDoorAndUnlock();
        }
        // Update is called once per frame
        void Update () {
		
        }

    }
}
