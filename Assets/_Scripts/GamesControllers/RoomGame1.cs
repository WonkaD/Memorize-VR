using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts.GamesControllers
{
    public class RoomGame1 : RoomGame
    {
        [SerializeField] private OfficeGameController _gameController;
        [SerializeField] private List<GameObject> _concreteGameObjects;
        [SerializeField] private GameObjectController _GameObjectController;
        [SerializeField] private int _minPoints = 0;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private EnumLevels _difficulty;

        private float _maxTimeSeconds;
        private long _showTimeMillis;
        private int _sizeLevel;
        private int _points;

        private int success;
        private float _timeLeft;
        private IEnumerator countDown;
        private List<GameObjectController> _generateObjectList = new List<GameObjectController>();
        private GameObjectController _selectedGameObject;
        private List<Vector3> positionList = new List<Vector3>();
        private int bonusMultiplier = 1;

        private GameObject MainCamera;

        // Update is called once per frame


        private void Awake()
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
            SetGameConfiguration();
            ResetGameRoom();

            StartScoreBoard();
            _gameController.StartLevel(0);
            //Show Result _showTimeMillis /1000 Seconds
            StartCoroutine(ShowResult());

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

        private IEnumerator ShowResult()
        {
            yield return new WaitForSeconds(2f);
            FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.ShowConcreteObject();
            yield return new WaitForSeconds(_showTimeMillis / 1000f);
            FadeCamera();
            foreach (var gameObjectController in _generateObjectList)
                gameObjectController.HideConcreteObject();


            StartCoroutine(countDown); //CountDown
        }

        private void SetGameConfiguration()
        {
            switch (_difficulty)
            {
                case EnumLevels.Easy:
                    _maxTimeSeconds = 10f;
                    _showTimeMillis = 3000;
                    _sizeLevel = 4;
                    break;
                case EnumLevels.Medium:
                    _maxTimeSeconds = 9f;
                    _showTimeMillis = 2500;
                    _sizeLevel = 6;
                    break;
                case EnumLevels.Hard:
                    _maxTimeSeconds = 7f;
                    _showTimeMillis = 2000;
                    _sizeLevel = 8;
                    break;
                case EnumLevels.Extreme:
                    _maxTimeSeconds = 5f;
                    _showTimeMillis = 1500;
                    _sizeLevel = 10;
                    break;
            }
        }

        private IEnumerator CountDownTimeLeft()
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
            StopCoroutine(countDown);
            _gameController.FinishLevel(0, new Punctuation(TimeStamp(), _points, _difficulty), _minPoints < _points);
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

        private void FadeCamera()
        {
            var vrCameraFade = MainCamera.GetComponent<VRCameraFade>();
            if (vrCameraFade == null)
                Debug.Log("VrCameraFade is NULL");
            else
                vrCameraFade.FadeIn(1, false);
        }

        public override void AbortGame()
        {
            StopCoroutine(countDown);
            _gameController.FinishLevel(0, new Punctuation(), false);
            Debug.Log("Abortando juego...");
        }
    }
}
