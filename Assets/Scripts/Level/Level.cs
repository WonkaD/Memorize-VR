using System;
using System.Collections.Generic;
using Assets.Scripts.GamesControllers;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class Level
    {
        [SerializeField] public RoomGame GameLevel;
        [SerializeField] public VRDoor LevelDoor;
        public List<Punctuation> RecordPunctuations = new List<Punctuation>();

    }
}