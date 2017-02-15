using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamesControllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Text Points;
        [SerializeField] private Text Time;
    

        public void SetTime(float time)
        {
            Time.text = "Time left: " + time.ToString("0.000");
        }
        public void SetScore(int points)
        {
            Points.text = "Points: "+ points;
        }
    }
}
