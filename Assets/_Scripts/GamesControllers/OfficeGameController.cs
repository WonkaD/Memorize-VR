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
        [SerializeField] private List<GameObject> _tutorials;
        [SerializeField] private readonly string _pathFile = "SaveGame.bin";
        [SerializeField] private ScoreBoard _scoreBoard;

        // Use this for initialization
        private void Start()
        {
            var gameStatus = Load();
            UnlockFirstLevel();
            RestoreGameStatus(gameStatus);
            _scoreBoard.UpdateScoreBoard(_gameLevels);
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
            var _saveGame = new GameStatus();
            foreach (var gameLevel in _gameLevels)
                _saveGame.LevelStates.Add(new LevelState(gameLevel));
            foreach (var tutorial in _tutorials)
                _saveGame.Tutorials.Add(tutorial == null);
            BinaryFileManager.Save(_pathFile, _saveGame);
            _scoreBoard.UpdateScoreBoard(_gameLevels);
        }

        private GameStatus Load()
        {
            return BinaryFileManager.Load<GameStatus>(_pathFile);
            //return _saveGame != null && _saveGame.LevelStates != null;
        }

        private void RestoreGameStatus(GameStatus saveGame)
        {
            for (var i = 0; i < saveGame.LevelStates.Count; i++)
                RestoreLevel(saveGame.LevelStates[i], _gameLevels[i]);
            for (var i = 0; i < saveGame.Tutorials.Count; i++)
                RestoreTutorial(saveGame.Tutorials[i], _tutorials[i]);
        }

        private void RestoreTutorial(bool saveGameTutorial, GameObject tutorial)
        {
            if (saveGameTutorial) Destroy(tutorial);
        }

        private static void RestoreLevel(LevelState savedLevel, Scripts.Level.Level gameLevel)
        {
            gameLevel.RecordPunctuations = savedLevel.Punctuations;
            gameLevel.LevelDoor.SetUnlock(savedLevel.Unlock);
        }

        public void StartLevel(int levelIndex)
        {
            _gameLevels[levelIndex].CloseDoorAndLock();
        }

        public void FinishLevel(int levelIndex, Punctuation punctuation, bool gameComplete)
        {
            var level = _gameLevels[levelIndex];
            level.OpenDoorAndUnlock();
            if (!gameComplete) return;

            UnlockNextLevelIfExist(levelIndex);
            level.AddPunctuation(punctuation);
            Save();
        }

        private void UnlockNextLevelIfExist(int actualLevelIndex)
        {
            if (++actualLevelIndex < _gameLevels.Count) _gameLevels[actualLevelIndex].UnlockLevel();
        }
    }
}
