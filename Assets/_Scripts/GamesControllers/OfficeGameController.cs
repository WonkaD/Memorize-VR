using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets._Scripts.Game;
using Assets._Scripts.Level;
using UnityEngine;

namespace Assets._Scripts.GamesControllers
{
    public class OfficeGameController : MonoBehaviour
    {
        [SerializeField] private List<Scripts.Level.Level> _gameLevels;
        private GameStatus _saveGame;
        public string PathFile = "SaveGame.bin";
        public ScoreBoard ScoreBoard;

        // Use this for initialization
        private void Start()
        {
            Load();
            UnlockFirstLevel();
            RestoreGameStatus();
            ScoreBoard.UpdateScoreBoard(_saveGame.LevelStates);
        }

        public List<Scripts.Level.Level> GetLevelsScoreRecords()
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
            var statusList = new List<LevelState>();
            foreach (var gameLevel in _gameLevels)
                statusList.Add(new LevelState(gameLevel));
            _saveGame.LevelStates = statusList;
            BinaryFileManager.Save(PathFile, _saveGame);
            ScoreBoard.UpdateScoreBoard(_saveGame.LevelStates);
        }

        private void Load()
        {
            _saveGame = BinaryFileManager.Load<GameStatus>(PathFile);
            //return _saveGame != null && _saveGame.LevelStates != null;
        }

        private void RestoreGameStatus()
        {
            var savedLevels = _saveGame.LevelStates;

            for (var i = 0; i < savedLevels.Count; i++)
                RestoreLevel(savedLevels[i], _gameLevels[i]);
        }

        private static void RestoreLevel(LevelState savedLevel, Scripts.Level.Level gameLevel)
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

            UnlockNextLevelIfExist(levelIndex);
            level.AddPunctuation(punctuation);
            level.OpenDoorAndUnlock();
            Save();
        }

        private void UnlockNextLevelIfExist(int actualLevelIndex)
        {
            if (++actualLevelIndex < _gameLevels.Count) _gameLevels[actualLevelIndex].UnlockLevel();
        }

    }
}
