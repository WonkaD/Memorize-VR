using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class OfficeGameController : MonoBehaviour
    {
        [SerializeField] private List<Level.Level> _gameLevels;
        private List<LevelStatus> _saveGame;
        public string PathFile = "saveGame.bin";
        public ScoreBoard ScoreBoard;

        // Use this for initialization
        private void Start()
        {
            if (!Load())
            {
                UnlockFirstLevel();
                ScoreBoard.UpdateScoreBoard(null);
            }
            ScoreBoard.UpdateScoreBoard(_saveGame);
        }

        public List<Level.Level> GetLevelsScoreRecords()
        {
            return _gameLevels;
        }

        private void UnlockFirstLevel()
        {
            var firstDoor = _gameLevels.FirstOrDefault();
            if (firstDoor != null) firstDoor.LevelDoor.SetUnlock(true);
        }

        private void Save()
        {
            var statusList = new List<LevelStatus>();
            foreach (var gameLevel in _gameLevels)
                statusList.Add(new LevelStatus(gameLevel));
            _saveGame = statusList;
            BinaryFileManager.Save(PathFile, _saveGame);
            //TODO  ScoreBoard
            ScoreBoard.UpdateScoreBoard(_saveGame);
        }

        private bool Load()
        {
            _saveGame = BinaryFileManager.Load(PathFile);
            var savedLevels = _saveGame;
            if (savedLevels == null || _gameLevels == null) return false;

            for (var i = 0; i < savedLevels.Count; i++)
                RestoreLevel(savedLevels[i], _gameLevels[i]);
            return true;
        }

        private static void RestoreLevel(LevelStatus savedLevel, Level.Level gameLevel)
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

        public void FinishLevel(int levelIndex, Punctuation punctuation, bool gameComplete)
        {
            var level = _gameLevels[levelIndex];
            level.OpenDoorAndUnlock();
            if (!gameComplete) return;

            UnlockNextLevel(levelIndex);
            level.AddPunctuation(punctuation);
            level.OpenDoorAndUnlock();
            Save();
        }

        private void UnlockNextLevel(int actualLevelIndex)
        {
            actualLevelIndex++;
            if (actualLevelIndex >= _gameLevels.Count) return;
            _gameLevels[actualLevelIndex].UnlockLevel();
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}
