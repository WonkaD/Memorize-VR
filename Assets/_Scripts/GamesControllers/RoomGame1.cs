﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GamesControllers
{
    public class RoomGame1 : RoomGame
    {
        [SerializeField] private OfficeGameController _gameController;
        [SerializeField] private List<GameObject> _concreteGameObjects;
        [SerializeField] private GameObjectController _GameObjectController;
        
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private AudioClip _winAudioClip;
        [SerializeField] private AudioClip _failAudioClip;


        private EnumLevels _difficulty;

        private float _maxTimeSeconds;
        private long _showTimeMillis;
        private int _sizeLevel;

        private readonly int _minPoints = 30;
        private int _points;
        private int bonusMultiplier = 1;

        private int success;
        private float _timeLeft;
        private IEnumerator countDown;
        private readonly List<GameObjectController> _generateObjectList = new List<GameObjectController>();
        private GameObjectController _selectedGameObject;
        private List<Vector3> positionList = new List<Vector3>();
        

        private Player player;
        private AudioSource AudioSource;

        private static readonly Dictionary<EnumLevels, DifficutySetting> DIFFICULTY_SETTINGS =
            new Dictionary<EnumLevels, DifficutySetting>
            {
                {EnumLevels.Easy, new DifficutySetting(15f, 3000, 4)},
                {EnumLevels.Medium, new DifficutySetting(18f, 3500, 6)},
                {EnumLevels.Hard, new DifficutySetting(23f, 4500, 8)},
                {EnumLevels.Extreme, new DifficutySetting(30f, 6500, 10)}
            };

        // Update is called once per frame


        private void Awake()
        {
            countDown = CountDownTimeLeft();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            AudioSource = GetComponent<AudioSource>();
        }

        private void Shuffle()
        {
            Utils.Shuffle(_concreteGameObjects);
            Utils.Shuffle(positionList);
        }

        private void GeneratePositionList()
        {
            //positionList = Utils.PosicionInPlane(1f, -10f, -2f, 0.5f, 3.5f, 1.5f); //Plane Position
            positionList = Utils.PosicionInCircle(new Vector3(-4.9f, 1.03f, 3.55f), -6f, 270 - DegreeStep() / 2,
                89 + DegreeStep() / 2, DegreeStep()); //Circle Position
        }

        private double DegreeStep()
        {
            return 180f / (_sizeLevel * 2);
        }

        public override void StartGame(EnumLevels difficulty)
        {
            _difficulty = difficulty;
            SetDifficultySettings(GetDifficultySettings());
            ResetGameRoom();

            StartScoreBoard();
            _gameController.StartLevel(0);
            //Show Result _showTimeMillis /1000 Seconds
            StartCoroutine(ShowSoluttion());

            Debug.Log("Empezando juego...");
        }

        private void ResetGameRoom()
        {
            if (countDown != null)
                StopCoroutine(countDown);
            ClearGameArea();
            bonusMultiplier = 1;
            _points = 0;
            success = 0;
            GeneratePositionList();
            GenerateGame();
            _timeLeft = _maxTimeSeconds;
            countDown = CountDownTimeLeft();
        }

        private void ClearGameArea()
        {
            foreach (var gameObjectController in _generateObjectList)
                Destroy(gameObjectController);
            _generateObjectList.Clear();
        }

        private void StartScoreBoard()
        {
            IncreasePointsIn(0);
            scoreController.SetTime(_timeLeft);
        }

        private void IncreasePointsIn(int points)
        {
            _points += points;
            scoreController.SetScore(_points);
        }

        private void GenerateGame()
        {
            Shuffle();
            var selectedGameObjectToCreate = _concreteGameObjects.Take(_sizeLevel).ToList();
            foreach (var concreteGameObject in selectedGameObjectToCreate)
            {
                _generateObjectList.Add(CreateGameObjectController(concreteGameObject, Utils.PopAt(positionList, 0)));
                _generateObjectList.Add(CreateGameObjectController(concreteGameObject, Utils.PopAt(positionList, 0)));
            }
        }

        private GameObjectController CreateGameObjectController(GameObject concreteGameObject, Vector3 position)
        {
            var vector3 = position /*+ RandomVector()*/;
            return Instantiate(_GameObjectController, transform).Init(this, concreteGameObject, vector3);
        }

        private static Vector3 RandomVector()
        {
            return new Vector3(RandomBetween1and_1(), RandomBetween1and_1(), RandomBetween1and_1());
        }

        private static float RandomBetween1and_1()
        {
            return Random.Range(-0.25f, 0.25f);
        }

        private IEnumerator ShowSoluttion()
        {
            yield return new WaitForSeconds(2f);
            player.FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.ShowConcreteObject();
            yield return new WaitForSeconds(_showTimeMillis / 1000f);
            player.FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.HideConcreteObject();


            StartCoroutine(countDown); //CountDown
        }

        private DifficutySetting GetDifficultySettings()
        {
            var difficuty = new DifficutySetting();
            DIFFICULTY_SETTINGS.TryGetValue(_difficulty, out difficuty);
            return difficuty;
        }

        private void SetDifficultySettings(DifficutySetting difficuty)
        {
            _maxTimeSeconds = difficuty.MaxTimeSeconds;
            _showTimeMillis = difficuty.ShowTimeMillis;
            _sizeLevel = difficuty.SizeLevel;
        }

        private IEnumerator CountDownTimeLeft()
        {
            while (_timeLeft > 0)
            {
                yield return new WaitForSeconds(0.001f * Time.time);
                _timeLeft -= 0.001f * Time.time;
                scoreController.SetTime(_timeLeft);
            }
            AbortGame();
        }

        public override void FinishGame()
        {
            StopCoroutine(countDown);
            AudioSource.PlayOneShot(_winAudioClip);
            _gameController.FinishLevel(0, new Punctuation(TimeStamp(), _points, _difficulty), _minPoints < _points);
            player.FadeCamera();
            ClearGameArea();
            Debug.Log("Finalizando juego...");
        }

        private float TimeStamp()
        {
            return _maxTimeSeconds - _timeLeft;
        }

        public override IEnumerator ClickEvent(GameObjectController selectedGameObject)
        {
            if (_selectedGameObject == null)
            {
                _selectedGameObject = selectedGameObject;
                yield break;
            }
            yield return new WaitForSeconds(0.4f);
            if (_selectedGameObject.SameConcreteObject(selectedGameObject))
                SameGameObjects(selectedGameObject);
            else
                NotSameGameObjects(selectedGameObject);
            _selectedGameObject = null;
        }

        private void NotSameGameObjects(GameObjectController selectedGameObject)
        {
            FailAnimation(selectedGameObject);
            FailurePoints();
        }

        private void SameGameObjects(GameObjectController selectedGameObject)
        {
            WinAnimation(selectedGameObject);
            SuccessPoints();
            CheckFinishGame();
        }

        private void FailAnimation(GameObjectController selectedGameObject)
        {
            _selectedGameObject.HideConcreteObject();
            selectedGameObject.HideConcreteObject();
        }

        private void CheckFinishGame()
        {
            success++;
            if (success == _sizeLevel)
                FinishGame();
        }

        private void WinAnimation(GameObjectController selectedGameObject)
        {
            _selectedGameObject.WinPosition();
            selectedGameObject.WinPosition();
        }

        private void FailurePoints()
        {
            IncreasePointsIn(-2);
            bonusMultiplier = 1;
        }

        private void SuccessPoints()
        {
            IncreasePointsIn(10 * bonusMultiplier);
            bonusMultiplier++;
        }

        public override void AbortGame()
        {
            StopCoroutine(countDown);
            AudioSource.PlayOneShot(_failAudioClip);
            _gameController.FinishLevel(0, new Punctuation(), false);
            player.FadeCamera();
            ClearGameArea();
            Debug.Log("Abortando juego...");
        }

        private class DifficutySetting
        {
            public readonly float MaxTimeSeconds;
            public readonly long ShowTimeMillis;
            public readonly int SizeLevel;

            public DifficutySetting()
            {
                MaxTimeSeconds = 15f;
                ShowTimeMillis = 3000;
                SizeLevel = 4;
            }

            public DifficutySetting(float maxTimeSeconds, long showTimeMillis, int sizeLevel)
            {
                MaxTimeSeconds = maxTimeSeconds;
                ShowTimeMillis = showTimeMillis;
                SizeLevel = sizeLevel;
            }
        }
    }
}
