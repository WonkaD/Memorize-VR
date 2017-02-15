using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts.GamesControllers
{
    public class GameObjectController : MonoBehaviour
    {
        private GameObject _visibleGameObject;
        [SerializeField] private GameObject _abstractObject;
        public GameObject ConcreteObject;
        private VRInteractiveItem _interactiveItem;
        private ParticleSystem _particle;
        private RoomGame _gameController;
        

        public void Init(RoomGame roomGame, GameObject concreteObject, Vector3 position)
        {
            _gameController = roomGame;
            ConcreteObject = concreteObject;
            transform.localPosition = position;
        }

        public void ShowConcreteObject()
        {
            ReplaceGameObject(ConcreteObject);
        }

        public void HideConcreteObject()
        {
            ReplaceGameObject(_abstractObject);
        }

        private void Awake()
        {
            _interactiveItem = gameObject.GetComponent<VRInteractiveItem>();
            _particle = gameObject.GetComponent<ParticleSystem>();
            _particle.Stop();
            ReplaceGameObject(_abstractObject);
            
        }


        private void ReplaceGameObject(GameObject newObject)
        {
            Destroy(_visibleGameObject);
            _visibleGameObject = Instantiate(newObject,gameObject.transform);
            _visibleGameObject.transform.localPosition = new Vector3(0,0,0);
        }

        private void OnEnable()
        {
            _interactiveItem.OnOver += HandleOver;
            _interactiveItem.OnOut += HandleOut;
            _interactiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            _interactiveItem.OnOver -= HandleOver;
            _interactiveItem.OnOut -= HandleOut;
            _interactiveItem.OnClick -= HandleClick;
            Destroy(gameObject);
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
            ReplaceGameObject(ConcreteObject); //TODO realizar alguna animación o algo, como transición.
            StartCoroutine(_gameController.ClickEvent(this));
        }

        protected bool Equals(GameObjectController other)
        {
            return ConcreteObject.Equals(other.ConcreteObject);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GameObjectController) obj);
        }

    }
}
