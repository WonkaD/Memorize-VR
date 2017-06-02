using System;
using UnityEngine;

namespace Assets._Scripts.Game
{
    [Serializable]
    public class Tutorial
    {
        public Tutorial(bool viewed, GameObject tutorialSign)
        {
            Viewed = viewed;
            TutorialSign = tutorialSign;
        }

        public bool Viewed;
        public GameObject TutorialSign;
        
    }
}