using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets.Scripts
{
    public class VRDoor : MonoBehaviour {

        [SerializeField] private VRInteractiveItem _interactiveItem;
        [SerializeField] private AudioClip _overAudio;
        [SerializeField] private AudioClip _selectAudio;
        [SerializeField] private Animator animator;
        [SerializeField] private bool _unlock = false;
        private bool _openDoor = false;
        private void OnEnable()
        {
            _interactiveItem.OnOver += HandleOver;
            _interactiveItem.OnOut += HandleOut;
            _interactiveItem.OnClick += HandleClick;
            _interactiveItem.OnDoubleClick += HandleDoubleClick;
        }


        private void OnDisable()
        {
            _interactiveItem.OnOver -= HandleOver;
            _interactiveItem.OnOut -= HandleOut;
            _interactiveItem.OnClick -= HandleClick;
            _interactiveItem.OnDoubleClick -= HandleDoubleClick;
        }

        public void OpenDoorAndUnlock()
        {
            SetUnlock(true);
            if (_openDoor) return;
            animator.SetTrigger("Action");
            _openDoor = true;
        }

        public void CloseDoorAndLock()
        {
            SetUnlock(false);
            if (!_openDoor) return;
            animator.SetTrigger("Action");
            _openDoor = false;
        }

        public void SetUnlock(bool status)
        {
            _unlock = status;
        }

        public bool GetUnlock()
        {
            return _unlock;
        }

        //Handle the Over event
        private void HandleOver()
        {
            //TODO reproducir sonido
            if (_unlock) gameObject.GetComponent<AudioSource>().PlayOneShot(_overAudio);
        }

        //Handle the Out event
        private void HandleOut()
        {
            //TODO pensar algo

        }

        //Handle the Click event
        private void HandleClick()
        {
            if (!_unlock) return;
            animator.SetTrigger("Action");
            _openDoor = !_openDoor;
            gameObject.GetComponent<AudioSource>().PlayOneShot(_selectAudio);
        }

        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            //TODO por ahora nada
        }
    }
}
