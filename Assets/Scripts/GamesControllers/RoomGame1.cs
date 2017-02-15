using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GamesControllers
{
    public class RoomGame1 : RoomGame {

        [SerializeField] private OfficeGameController _gameController;
        [SerializeField] private List <GameObject> _concreteGameObjects;
        [SerializeField] private GameObject _abstractGameObjects;

        [SerializeField] private int _minPoints = 0;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private EnumLevels difficulty;

        private float _maxTimeSeconds = 0;
        private long _showTimeMillis = 0;
        private int points = 0;
        private float timeLeft = 0.0f;


        // Update is called once per frame

        void Update () {
		
        }

        public override void StartGame()
        {
            _gameController.StartLevel(0);
            SetGameConfiguration();
            timeLeft = _maxTimeSeconds;
            //Show result
            //Wait
            //Hide result

            scoreController.SetScore(points);
            scoreController.SetTime(timeLeft);
            StartCoroutine(CountDown()); //CountDown
            Debug.Log("Empezando juego...");

        }

        private void SetGameConfiguration()
        {
            switch (difficulty)
            {
                case EnumLevels.Easy:
                    _maxTimeSeconds = 60f;
                    _showTimeMillis = 1000;
                    break;
                case EnumLevels.Medium:
                    _maxTimeSeconds = 50f;
                    _showTimeMillis = 800;
                    break;
                case EnumLevels.Hard:
                    _maxTimeSeconds = 40f;
                    _showTimeMillis = 750;
                    break;
                case EnumLevels.Extreme:
                    _maxTimeSeconds = 30f;
                    _showTimeMillis = 500;
                    break;

            }
        }

        IEnumerator CountDown()
        {
            while (timeLeft > 0)
            {
                yield return new WaitForSeconds(0.001f);
                timeLeft -= 0.001f;
                scoreController.SetTime(timeLeft);
            }
            FinishGame();
        }

        public override void FinishGame()
        {
            _gameController.FinishLevel(0, new Punctuation(TimeStamp(), points, difficulty), _minPoints<points);
            Debug.Log("Finalizando juego...");
        }

        private float TimeStamp()
        {
            return _maxTimeSeconds - timeLeft;
        }
    }
}
