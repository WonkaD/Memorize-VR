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

        [SerializeField] public RoomGame RoomGame;
        [SerializeField] public VRDoor DoorOfLevel;
        public List<Punctuation> RecordPunctuations = new List<Punctuation>();

        public void AddPunctuation(Punctuation punctuation)
        {
            RecordPunctuations.Add(punctuation);
        }

        public void OpenDoorAndUnlock()
        {
            DoorOfLevel.OpenDoorAndUnlock();
        }

        public void CloseDoorAndLock()
        {
            DoorOfLevel.CloseDoorAndLock();
        }

        public void UnlockLevel()
        {
            DoorOfLevel.SetUnlock(true);
        }
    }
}