using System;
using System.Collections.Generic;
using Assets._Scripts.Level;

namespace Assets._Scripts.Game
{
    [Serializable]
    public class GameStatus
    {
        public List<LevelState> LevelStates;
        public List<bool> TutorialStates;


        public GameStatus(List<LevelState> levelStates, List<bool> tutorialStates)
        {
            LevelStates = levelStates;
            TutorialStates = tutorialStates;

        }

        public GameStatus()
        {
            LevelStates = new List<LevelState>();
            TutorialStates = new List<bool>();
        }

    }
}
