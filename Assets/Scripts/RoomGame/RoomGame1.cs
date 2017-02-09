using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoomGame1 : RoomGame {

        [SerializeField] private OfficeGameController _gameController;
        [SerializeField] private List <GameObject> _containers;
        [SerializeField] private long _maxTimeMillis;
        [SerializeField] private long _showTimeMillis;





        // Use this for initialization
        void Start () {
		    
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        public override void StartGame()
        {
            _gameController.StartLevel(0);
            Debug.Log("Empezando juego...");

        }

        public override void FinishGame()
        {
            _gameController.FinishLevel(0, null);
            Debug.Log("Finalizando juego...");
        }
        public void FinishGame2()
        {
            Debug.Log("Finalizando juego bien...");
            _gameController.FinishLevel(0, new Punctuation(1,50));
        }
    }
}
