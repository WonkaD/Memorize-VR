using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Level
    {
        [SerializeField] public RoomGame GameLevel;
        [SerializeField] public VRDoor LevelDoor;
        public List<Punctuation> RecordPunctuations = new List<Punctuation>();

    }
}