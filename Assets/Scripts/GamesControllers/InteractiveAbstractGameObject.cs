using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts.GamesControllers
{
    public class InteractiveAbstractGameObject : MonoBehaviour
    {
        private GameObject showGameObject;
        [SerializeField] private GameObject _AbstractObject;
        [SerializeField] private GameObject _ConcreteObject;
        [SerializeField] private VRInteractiveItem interactiveItem;

        private bool isSelected;
        public int hiddenValue;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                if (!value) ReplaceGameObject(_AbstractObject);
            }
        }

        private void Awake()
        {
            ReplaceGameObject(_AbstractObject);
        }

        private void ReplaceGameObject(GameObject newObject)
        {
            Destroy(showGameObject);
            showGameObject = Instantiate(newObject,new Vector3(0,0,0),new Quaternion(),transform);
        }

        private void OnEnable()
        {
            interactiveItem.OnOver += HandleOver;
            interactiveItem.OnOut += HandleOut;
            interactiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            interactiveItem.OnOver -= HandleOver;
            interactiveItem.OnOut -= HandleOut;
            interactiveItem.OnClick -= HandleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            //Do something
        }


        //Handle the Out event
        private void HandleOut()
        {
            //Do something
        }


        //Handle the Click event
        private void HandleClick()
        {
            //Show concrete object
            isSelected = true;
            ReplaceGameObject(_ConcreteObject);

        }

    }
}
