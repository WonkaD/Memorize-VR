using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class Level
    {
        public RoomGame gameLevel;
        public VRDoor levelDoor;
        public List<Punctuation> recordPunctuations;

    }
}