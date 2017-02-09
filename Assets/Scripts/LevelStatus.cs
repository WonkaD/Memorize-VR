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
            Unlock = gameLevel.levelDoor.GetUnlock();
            Punctuations = gameLevel.recordPunctuations;
        }

        public LevelStatus()
        {
            Unlock = false;
            Punctuations = new List<Punctuation>();
        }
    }
}