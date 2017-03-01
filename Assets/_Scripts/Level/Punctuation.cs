using System;

namespace Assets.Scripts
{
    [Serializable]
    public class Punctuation
    {
        public DateTime date;
        public float timeStamp;
        public int points;
        public EnumLevels difficulty;

        public Punctuation()
        {
            date = DateTime.Now.ToUniversalTime();
            timeStamp = 0;
            points = 0;
            difficulty = EnumLevels.Easy;
        }

        public Punctuation(float timeStamp, int points, EnumLevels difficulty)
        {
            date = DateTime.Now.ToUniversalTime();
            this.timeStamp = timeStamp;
            this.points = points;
            this.difficulty = difficulty;
        }

    }
}