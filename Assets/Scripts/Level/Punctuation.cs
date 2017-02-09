using System;

namespace Assets.Scripts
{
    [Serializable]
    public class Punctuation
    {
        public DateTime date;
        public long timeStamp;
        public int points;

        public Punctuation()
        {
            date = DateTime.Now.ToUniversalTime();
            timeStamp = 0;
            points = 0;
        }

        public Punctuation(long timeStamp, int points)
        {
            date = DateTime.Now.ToUniversalTime();
            this.timeStamp = timeStamp;
            this.points = points;
        }
    }
}