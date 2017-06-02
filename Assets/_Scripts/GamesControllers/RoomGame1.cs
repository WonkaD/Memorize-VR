using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.GamesControllers;
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
        [SerializeField] private ControlPanelGame _controlPanelGame;


        private EnumLevels _difficulty;
        private DifficutySetting _difficutySetting;
        private Punctuation _punctuation;

        private int _success;
        private float _timeLeft;
        private IEnumerator _countDown;
        private readonly List<GameObjectController> _generateObjectList = new List<GameObjectController>();
        private GameObjectController _selectedGameObject;
        private List<Vector3> positionList = new List<Vector3>();

        private Player player;
        private AudioSource AudioSource;

        private static readonly Dictionary<EnumLevels, DifficutySetting> DIFFICULTY_SETTINGS =
            new Dictionary<EnumLevels, DifficutySetting>
            {
                {EnumLevels.Easy, new DifficutySetting(15f, 3000, 4)},
                {EnumLevels.Medium, new DifficutySetting(20f, 3500, 6)},
                {EnumLevels.Hard, new DifficutySetting(30f, 6000, 8)},
                {EnumLevels.Extreme, new DifficutySetting(35f, 7000, 10)}
            };


        private void Awake()
        {
            _countDown = CountDownTimeLeft();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            AudioSource = GetComponent<AudioSource>();
        }

        #region GameController

        public override void StartGame(EnumLevels difficulty)
        {
            _difficulty = difficulty;
            _difficutySetting = GetDifficultySettings();
            ResetGameRoom();
            _gameController.StartLevel(0);
            StartCoroutine(ShowSoluttion());
        }

        public override void FinishGame()
        {
            StopCoroutine(_countDown);
            AudioSource.PlayOneShot(_winAudioClip);
            _gameController.FinishLevel(0, new _Scripts.Level.Punctuation(TimeStamp(), _punctuation.Points, _difficulty),
                _punctuation.IsSuccessful());
            ClearGameArea();
        }

        public override void AbortGame()
        {
            StopCoroutine(_countDown);
            AudioSource.PlayOneShot(_failAudioClip);
            _gameController.FinishLevel(0, new _Scripts.Level.Punctuation(), false);
            ClearGameArea();
        }

        #endregion


        private void ResetGameRoom()
        {
            if (_countDown != null) StopCoroutine(_countDown);
            ClearGameArea();
            _punctuation = new Punctuation();
            _success = 0;
            GeneratePositionList();
            GenerateGame();
            _timeLeft = _difficutySetting.MaxTimeSeconds;
            _countDown = CountDownTimeLeft();
            ResetScoreBoard();
        }

        private void ClearGameArea()
        {
            player.FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                Destroy(gameObjectController);
            _generateObjectList.Clear();
            _controlPanelGame.OpenPanel();
        }


        private void ResetScoreBoard()
        {
            scoreController.SetScore(0);
            scoreController.SetTime(_timeLeft);
        }

        private void GenerateGame()
        {
            Shuffle();
            var selectedGameObjectToCreate = _concreteGameObjects.Take(_difficutySetting.SizeLevel).ToList();
            foreach (var concreteGameObject in selectedGameObjectToCreate)
            {
                _generateObjectList.Add(CreateGameObjectController(concreteGameObject, Utils.PopAt(positionList, 0)));
                _generateObjectList.Add(CreateGameObjectController(concreteGameObject, Utils.PopAt(positionList, 0)));
            }
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
            return 180f / (_difficutySetting.SizeLevel * 2);
        }

        private GameObjectController CreateGameObjectController(GameObject concreteGameObject, Vector3 position)
        {
            var vector3 = position /*+ RandomVector()*/;
            return Instantiate(_GameObjectController, transform).Init(this, concreteGameObject, vector3);
        }

        private IEnumerator ShowSoluttion()
        {
            yield return new WaitForSeconds(2f);
            player.FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.ShowConcreteObject();
            yield return new WaitForSeconds(_difficutySetting.ShowTimeMillis / 1000f);
            player.FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.HideConcreteObject();


            StartCoroutine(_countDown); //CountDown
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

        

        public override IEnumerator ClickEvent(GameObjectController selectedGameObject)
        {
            if (_selectedGameObject == null)
                _selectedGameObject = selectedGameObject;
            else
            {
                yield return new WaitForSeconds(0.4f);
                CompareSelectedObjects(selectedGameObject);
            }
        }

        private void CompareSelectedObjects(GameObjectController selectedGameObject)
        {
            if (_selectedGameObject != null && _selectedGameObject.IsSameConcreteObject(selectedGameObject)) Success(selectedGameObject);
            else Fail(selectedGameObject);
            _selectedGameObject = null;
        }

        private void Fail(GameObjectController selectedGameObject)
        {
            FailAnimation(selectedGameObject);
            _punctuation.FailurePoints();
            scoreController.SetScore(_punctuation.Points);
        }

        private void Success(GameObjectController selectedGameObject)
        {
            WinAnimation(selectedGameObject);
            _punctuation.SuccessPoints();
            scoreController.SetScore(_punctuation.Points);
            CheckFinishGame();
        }

        private void CheckFinishGame()
        {
            if (++_success == _difficutySetting.SizeLevel) FinishGame();
        }

        #region Utils

        private float TimeStamp()
        {
            return _difficutySetting.MaxTimeSeconds - _timeLeft;
        }

        private DifficutySetting GetDifficultySettings()
        {
            return DIFFICULTY_SETTINGS[_difficulty];
        }

        #endregion

        #region Animation

        private void FailAnimation(GameObjectController selectedGameObject)
        {
            _selectedGameObject.HideConcreteObject();
            selectedGameObject.HideConcreteObject();
        }

        private void WinAnimation(GameObjectController selectedGameObject)
        {
            _selectedGameObject.WinPosition();
            selectedGameObject.WinPosition();
        }

        #endregion


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

        private class Punctuation
        {
            private readonly int _minPoints = 30;
            public int Points;
            private int _bonusMultiplier = 1;

            public void FailurePoints()
            {
                IncreasePointsIn(-2);
                _bonusMultiplier = 1;
            }

            public void SuccessPoints()
            {
                IncreasePointsIn(10 * _bonusMultiplier);
                _bonusMultiplier++;
            }

            private void IncreasePointsIn(int points)
            {
                Points += points;
            }

            public bool IsSuccessful()
            {
                return _minPoints < Points;
            }
        }
    }
}
