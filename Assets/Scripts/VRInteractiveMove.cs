using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts
{
    public class VRInteractiveMove : MonoBehaviour {

        [SerializeField]
        private Material normalMaterial;
        [SerializeField]
        private Material overMaterial;
        [SerializeField]
        private VRInteractiveItem interactiveItem;
        [SerializeField]
        private Renderer objectRenderer;
        [SerializeField]
        private Transform objectTransform;

        [SerializeField] private RectTransform pointerTransform;

        private Transform firstTransform;

        private void Awake()
        {
            objectRenderer.material = normalMaterial;
            firstTransform = objectTransform;
        }


        private void OnEnable()
        {
            interactiveItem.OnOver += HandleOver;
            interactiveItem.OnOut += HandleOut;
            interactiveItem.OnDown += HandleDown;
            interactiveItem.OnDoubleClick += HandleDoubleClick;
        }


        private void OnDisable()
        {
            interactiveItem.OnOver -= HandleOver;
            interactiveItem.OnOut -= HandleOut;
            interactiveItem.OnDown -= HandleDown;
            interactiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");
            objectRenderer.material = overMaterial;

        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            objectRenderer.material = normalMaterial;

        }


        //Handle the Click event
        private void HandleDown()
        {
            Debug.Log("Show click state");
            var newPosition = new Vector3(pointerTransform.position.x, pointerTransform.position.y, objectTransform.position.z);
            objectTransform.position = newPosition;
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            objectTransform = firstTransform;
        }
    }
}
