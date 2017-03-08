using System;
using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts.GamesControllers
{
    public class GameObjectController : MonoBehaviour
    {
        public GameObject GameObjectContainer;
        private GameObject _visibleGameObject;
        public AudioSource AudioSource;
        public AudioClip ReplaceClip;
        [SerializeField] private GameObject _abstractObject;
        [HideInInspector] public GameObject ConcreteObject;
        [SerializeField] private VRInteractiveItem _interactiveItem;
        [SerializeField] private ParticleSystem _particle;
        private RoomGame _gameController;
        

        public GameObjectController Init(RoomGame roomGame, GameObject concreteObject, Vector3 position)
        {
            _gameController = roomGame;
            ConcreteObject = concreteObject;
            transform.localPosition = position;
            return this;
        }

        public void ShowConcreteObject()
        {
            ReplaceGameObject(ConcreteObject);
            DisableEvents();
        }

        public void HideConcreteObject()
        {
            ReplaceGameObject(_abstractObject);
            EnableEvents();
        }

        private void Awake()
        {
            _particle.Stop();
            ReplaceGameObject(_abstractObject);
            
        }


        private void ReplaceGameObject(GameObject newObject)
        {
            
            Destroy(_visibleGameObject);
            _visibleGameObject = Instantiate(newObject, GameObjectContainer.transform);
            _visibleGameObject.transform.localPosition = new Vector3(0,0,0);
        }

        private void OnEnable()
        {
            EnableEvents();
        }

        private void EnableEvents()
        {
            _interactiveItem.OnOver += HandleOver;
            _interactiveItem.OnOut += HandleOut;
            _interactiveItem.OnClick += HandleClick;
        }

        private void OnDisable()
        {
            DisableEvents();
            Destroy(gameObject);
        }

        private void DisableEvents()
        {
            _interactiveItem.OnOver -= HandleOver;
            _interactiveItem.OnOut -= HandleOut;
            _interactiveItem.OnClick -= HandleClick;
        }

        public void WinPosition()
        {
            DisableEvents();
            GameObjectContainer.transform.Translate(Vector3.up * 2f);
        }


        //Handle the Over event
        private void HandleOver()
        {
            _particle.Play();
        }


        //Handle the Out event
        private void HandleOut()
        {
            _particle.Stop();
        }


        //Handle the Click event
        private void HandleClick()
        {
            //Show concrete object
            AudioSource.PlayOneShot(ReplaceClip);
            ReplaceGameObject(ConcreteObject); //TODO realizar alguna animación o algo, como transición.
            DisableEvents();
            StartCoroutine(_gameController.ClickEvent(this));
        }

        public bool SameConcreteObject(GameObjectController other)
        {
            return ConcreteObject.Equals(other.ConcreteObject);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var obj2 = obj as GameObjectController;
            return (obj2 != null && obj2.ConcreteObject.Equals(ConcreteObject) && obj2.transform.localPosition.Equals(transform.localPosition));
        }



    }
}
