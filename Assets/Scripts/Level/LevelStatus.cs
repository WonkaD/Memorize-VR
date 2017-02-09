using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class LevelStatus
    {
        public bool Unlock;
        public List<Punctuation> Punctuations;
            
        public LevelStatus(Level gameLevel)
        {
            Unlock = gameLevel.LevelDoor.GetUnlock();
            Punctuations = gameLevel.RecordPunctuations;
        }

        public LevelStatus()
        {
            Unlock = false;
            Punctuations = new List<Punctuation>();
        }
    }
}