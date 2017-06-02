using System;
using System.Collections.Generic;
using Assets._Scripts.Level;

namespace Assets._Scripts.Game
{
    [Serializable]
    public class GameStatus
    {
        public List<LevelState> LevelStates;
        public List<Tutorial> Tutorials;


        public GameStatus(List<LevelState> levelStates, List<Tutorial> tutorials)
        {
            LevelStates = levelStates;
            Tutorials = tutorials;

        }

        public GameStatus()
        {
            LevelStates = new List<LevelState>();
            Tutorials = new List<Tutorial>();
        }

    }
}
