using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using VRStandardAssets.Utils;
using Random = System.Random;


namespace Assets.Scripts.GamesControllers
{
    public class RoomGame1 : RoomGame {

        [SerializeField] private OfficeGameController _gameController;
        [SerializeField] private List <GameObject> _concreteGameObjects;
        [SerializeField] private GameObjectController _GameObjectController;
        [SerializeField] private int _minPoints = 0;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private EnumLevels difficulty;
        public float RightLimit;
        public float UpLimit;


        private float _maxTimeSeconds;
        private long _showTimeMillis;
        private int _sizeLevel = 0;
        private int _points = 0;
        private float _timeLeft;
        private GameObjectController Selected;
        private List<Vector3> positionList  = new List<Vector3>();
        private int bonusMultiplier = 1;

        private GameObject MainCamera;

        // Update is called once per frame

        private void Awake()
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            GeneratePositionList();
            Utils.Shuffle(_concreteGameObjects);
            Utils.Shuffle(positionList);

        }

        private void GeneratePositionList()
        {
            IEnumerable<float> yPosition = Utils.FloatRange(0.5f, 3.5f, 1.5f);
            IEnumerable<float> xPosition = Utils.FloatRange(1f, -10f, -2f);
            foreach (var x in xPosition)
            {
                var vector3 = new Vector3(x, 0, 0);
                foreach (var y in yPosition)
                {
                    positionList.Add(vector3 + new Vector3(0, y, 0));
                }
            }
        }


        public override void StartGame()
        {
            _gameController.StartLevel(0);
            SetGameConfiguration();
            GenerateGame();
            _timeLeft = _maxTimeSeconds;

            StartCoroutine(ShowResult());

            IncreasePointsIn(0);
            scoreController.SetTime(_timeLeft);
            StartCoroutine(CountDownTimeLeft()); //CountDown
            Debug.Log("Empezando juego...");

        }

        private void IncreasePointsIn(int points)
        {
            _points += points;
            scoreController.SetScore(_points);
        }

        private void GenerateGame()
        {
            for (int i = 0; i < _sizeLevel; i++)
            {
                var concreteGameObject = _concreteGameObjects[i];
                Instantiate(_GameObjectController, transform).Init(this, concreteGameObject, positionList[i]);
                Instantiate(_GameObjectController, transform).Init(this, concreteGameObject, positionList[positionList.Count-i-1]);
            }

        }

        IEnumerator ShowResult()
        {
            GameObjectController[] gameObjectControllers = GetComponentsInChildren<GameObjectController>();
            foreach (var gameObjectController in gameObjectControllers)
            {
                gameObjectController.ShowConcreteObject();
            }
            yield return new WaitForSeconds(_showTimeMillis / 1000f);
            FadeCamera();
            foreach (var gameObjectController in gameObjectControllers)
            {
                gameObjectController.HideConcreteObject();
            }
        }

        private void SetGameConfiguration()
        {

            switch (difficulty)
            {
                case EnumLevels.Easy:
                    _maxTimeSeconds = 60f;
                    _showTimeMillis = 1500;
                    _sizeLevel = 5;
                    break;
                case EnumLevels.Medium:
                    _maxTimeSeconds = 50f;
                    _showTimeMillis = 1350;
                    _sizeLevel = 6;
                    break;
                case EnumLevels.Hard:
                    _maxTimeSeconds = 40f;
                    _showTimeMillis = 1250;
                    _sizeLevel = 8;
                    break;
                case EnumLevels.Extreme:
                    _maxTimeSeconds = 30f;
                    _showTimeMillis = 1000;
                    _sizeLevel = 10;
                    break;
            }
        }

        IEnumerator CountDownTimeLeft()
        {
            while (_timeLeft > 0)
            {
                yield return new WaitForSeconds(0.001f);
                _timeLeft -= 0.001f;
                scoreController.SetTime(_timeLeft);
            }
            FinishGame();
        }

        public override void FinishGame()
        {
            _gameController.FinishLevel(0, new Punctuation(TimeStamp(), _points, difficulty), _minPoints<_points);
            Debug.Log("Finalizando juego...");
        }

        private float TimeStamp()
        {
            return _maxTimeSeconds - _timeLeft;
        }

        public override IEnumerator ClickEvent(GameObjectController gameObjectController)
        {
            if (Selected == null)
            {
                Selected = gameObjectController;
                yield break;
            }
            yield return new WaitForSeconds(0.7f);
            if (Selected.Equals(gameObjectController))
            {
                Destroy(Selected);
                Destroy(gameObjectController);
                IncreasePointsIn(10* bonusMultiplier);
                bonusMultiplier++;

            }else{
                Selected.HideConcreteObject();
                gameObjectController.HideConcreteObject();
                IncreasePointsIn(-2);
                bonusMultiplier = 1;
            }
            Selected = null;

        }

        private void FadeCamera()
        {
            var vrCameraFade = MainCamera.GetComponent<VRCameraFade>();
            if (vrCameraFade == null)
                Debug.Log("VrCameraFade is NULL");
            else
                vrCameraFade.FadeIn(1, false);
        }

    }
}
