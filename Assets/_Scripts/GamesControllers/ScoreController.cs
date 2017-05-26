using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GamesControllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Text _points;
        [SerializeField] private Text _time;
    

        public void SetTime(float time)
        {
            _time.text = "Time left: " + time.ToString("0.000");
        }
        public void SetScore(int points)
        {
            _points.text = "Points: "+ points;
        }
    }
}
