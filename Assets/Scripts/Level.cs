using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Level
    {
        public RoomGame gameLevel;
        public VRDoor levelDoor;
        public List<Punctuation> recordPunctuations;

        public class Punctuation
        {
            private DateTime date;
            private long timeStamp;
            private int points;
        }
    }
}