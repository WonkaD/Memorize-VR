using System;
using System.Collections.Generic;
using Assets.Scripts.GamesControllers;
using Assets._Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class Level
    {

        [SerializeField] public RoomGame GameLevel;
        [SerializeField] public VRDoor LevelDoor;
        public List<Punctuation> RecordPunctuations = new List<Punctuation>();

        public void AddPunctuation(Punctuation punctuation)
        {
            RecordPunctuations.Add(punctuation);
        }

        public void OpenDoorAndUnlock()
        {
            LevelDoor.OpenDoorAndUnlock();
        }

        public void CloseDoorAndLock()
        {
            LevelDoor.CloseDoorAndLock();
        }

        public void UnlockLevel()
        {
            LevelDoor.SetUnlock(true);
        }
    }
}