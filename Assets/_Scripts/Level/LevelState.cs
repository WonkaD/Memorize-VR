using System;
using System.Collections.Generic;
using Assets.Scripts;

namespace Assets._Scripts.Level
{
    [Serializable]
    public class LevelState
    {
        public bool Unlock;
        public List<Punctuation> Punctuations;
            
        public LevelState(Scripts.Level.Level gameLevel)
        {
            Unlock = gameLevel.DoorOfLevel.GetUnlock();
            Punctuations = gameLevel.RecordPunctuations;
        }

        public LevelState()
        {
            Unlock = false;
            Punctuations = new List<Punctuation>();
        }
    }
}