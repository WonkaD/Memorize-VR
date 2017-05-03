using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

namespace Assets.Scripts
{
    [Serializable]
    public class Punctuation : IEquatable<Punctuation>, IComparable<Punctuation>
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

        private float GetTotalPunctuation(Punctuation punctuation)
        {
            return punctuation.points * getBonusForLevel(punctuation.difficulty) / punctuation.timeStamp;
        }

        private float getBonusForLevel(EnumLevels difficulty)
        {
            return 1 + (int)difficulty / 2f;
        }

        public bool Equals(Punctuation other)
        {
            if (other == null) return false;
            return date.Equals(other.date);
        }

        public int CompareTo(Punctuation other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(null, this)) return -1;
            return GetTotalPunctuation(other).CompareTo(GetTotalPunctuation(this));
        }
    }
}