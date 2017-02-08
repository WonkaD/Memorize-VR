using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Level
    {
        [SerializeField] public RoomGame gameLevel;
        [SerializeField] public VRDoor levelDoor;
        public List<Punctuation> recordPunctuations = new List<Punctuation>();

    }
}